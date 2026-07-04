using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class HandManager2 : MonoBehaviour
{
   
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardAtttackPrefab;
    [SerializeField] private GameObject cardBuffPrefab;
    [SerializeField] private GameObject cardDebuffPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;
    private readonly List<GameObject> handCards = new();


    // Urutan draw berulang: Attack -> Buff -> Debuff
    // drawIndex selalu bertambah setiap kartu berhasil di-draw.
    private int drawIndex;
    // Urutan draw harus konsisten bahkan jika ada kartu yang sudah dihancurkan.
    // Jadi, drawIndex hanya berubah ketika benar-benar menambah kartu baru ke hand.



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DrawCard();
    }

    private void DrawCard()
    {
        // Bersihkan referensi kartu yang sudah dihancurkan/dihapus.
        int beforeCount = handCards.Count;
        handCards.RemoveAll(c => c == null);
        int removedCount = beforeCount - handCards.Count;

        if (handCards.Count >= maxHandSize) return;

        // Kalau kartu dihapus/destroy, urutan "berikutnya" harus tetap seperti seolah-olah kartu itu
        // tidak pernah ada di hand.
        // Jadi sinkronkan drawIndex dengan jumlah draw yang sudah sesuai kartu aktif.
        // Dengan pola 3-step, drawIndex dihitung ulang berdasarkan jumlah draw yang sudah dilakukan
        // minus jumlah kartu yang hilang dari hand.
        if (removedCount > 0)
        {
            // drawIndex merepresentasikan langkah ke-kartu berikutnya yang akan digambar.
            // Jika kartu dihapus saat hand full, maka urutan harus “mengulang dari kartu yang terhapus”.
            // Cara sederhana: gunakan drawIndex berdasarkan jumlah kartu aktif agar langkah berikutnya
            // mengambil tipe kartu yang sama seperti posisi slot yang hilang.
            // (drawIndex diubah hanya ketika ada kartu yang hilang.)
            drawIndex = handCards.Count;

        }


        // Prioritas sesuai urutan, tapi jika prefab salah satu tipe tidak ada (null),
        // tipe tersebut dilewati dan digantikan oleh tipe berikutnya yang tersedia.
        // Urutan base: Attack -> Buff -> Debuff
        TidakFantasthCardType nextType = GetNextAvailableType();

        GameObject prefab = GetPrefabForType(nextType);
        if (prefab == null)
            return; // semua prefab null

        GameObject g = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        handCards.Add(g);


        var cardComponent = g.GetComponent<TidakFantasth.Card2>();
        if (cardComponent != null)
        {
            cardComponent.SetOwnerHand(this);
            cardComponent.cardType = (TidakFantasth.Card2.CardType)nextType;
        }

        // increment setelah berhasil draw
        drawIndex++;

        UpdateCardPosition();
    }

    // Helper: urutan draw berulang
    private enum TidakFantasthCardType
    {
        Attack = 0,
        Buff = 1,
        Debuff = 2
    }




    private TidakFantasthCardType GetNextAvailableType()
    {
        // Coba urutan dari drawIndex%3 sampai 3 kali.
        // Ini memastikan “yang terakhir debuff” tetap benar selama debuff tersedia,
        // dan jika tidak tersedia maka diganti tipe berikutnya yang ada.
        for (int i = 0; i < 3; i++)
        {
            int candidate = (drawIndex + i) % 3;
            if (IsTypeAvailable((TidakFantasthCardType)candidate))
                return (TidakFantasthCardType)candidate;
        }

        return TidakFantasthCardType.Attack; // fallback (akan ditangani oleh prefab==null)
    }

    private bool IsTypeAvailable(TidakFantasthCardType type)
    {
        return type switch
        {
            TidakFantasthCardType.Attack => cardAtttackPrefab != null,
            TidakFantasthCardType.Buff => cardBuffPrefab != null,
            TidakFantasthCardType.Debuff => cardDebuffPrefab != null,
            _ => false
        };
    }

    private GameObject GetPrefabForType(TidakFantasthCardType type)
    {
        return type switch
        {
            TidakFantasthCardType.Attack => cardAtttackPrefab,
            TidakFantasthCardType.Buff => cardBuffPrefab,
            TidakFantasthCardType.Debuff => cardDebuffPrefab,
            _ => null
        };
    }


    public void ReturnCardToHand(TidakFantasth.Card2 card)
    {
        // Karena layout hand ditentukan oleh UpdateCardPosition(), panggil ulang untuk mengembalikan ke posisi semula (berdasarkan index di hand).
        UpdateCardPosition();
    }

    private void UpdateCardPosition()
    {
        handCards.RemoveAll(c => c == null);
        if (handCards.Count == 0) return;

        float cardSpacing = 1f / maxHandSize; // Adjust this value for spacing between cards
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2; // Center the cards around the middle of the spline
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward)).normalized;

            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }
    public void RemoveCardFromHand(TidakFantasth.Card2 card)
    {
    // 1. Periksa apakah kartu tersebut ada di dalam list tangan, jika ada maka hapus
    if (handCards.Contains(card.gameObject))
    {
        handCards.Remove(card.gameObject);
    }

    // 2. Atur ulang posisi kartu-kartu yang tersisa di tangan agar otomatis merapat kembali
    UpdateCardPosition();
    }

}
