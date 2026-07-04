using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TidakFantasth {
public class Card2 : MonoBehaviour
{
    // Start is called before the first frame update
   
       
        public GameObject atkPrefab;
        public GameObject buffPrefab;
        public GameObject debuffPrefab;
        public string cardName;
        public CardType cardType;
        public int damage;
        public static bool isDragging;
        public bool isActivated;
        public bool isInDropZone = false;
        
        [HideInInspector] public Vector3 offset;
        [HideInInspector] public HandManager2 ownerHand;

        // Hook sementara untuk membuktikan "buff menambahkan damage attack x2".
        // - Saat Buff dipakai, set flag sehingga Attack berikutnya memberikan damage x2.
        public static bool NextAttackIsBuffed { get; private set; }

        void Start()
        {
            isDragging = false;
            isActivated = false;
        }

        public enum CardType
        {
            Attack,
            Buff,
            Debuff
        }

        void Update()
        {
            if (Input.GetKeyDown("q"))
            {
                Destroy(gameObject);
            }

            if (isActivated == true)
            {
                ApplyCardEffect();
                Destroy(gameObject);
            }
        }

        public void SetOwnerHand(HandManager2 hand)
        {
            ownerHand = hand;
        }

        
        void OnMouseDown()
        {
            offset = transform.position - GetMouseWorldPos();
        }

        void OnMouseDrag()
        {
            transform.position = GetMouseWorldPos() + offset;
        }

        void OnMouseUp()
        {
            // Efek kartu hanya aktif kalau kartu dilepas di panel Drop Kartu.
            // Saat ini belum ada referensi/ID untuk panel drop, jadi:
            // - jika script ini ditempel di area panel drop, kamu bisa panggil ActivateFromDrop()
            // - jika tidak, efek tidak otomatis jalan.
            Collider2D hit = Physics2D.OverlapPoint(transform.position);

        if (hit != null)
        {
            DropKartu drop = hit.GetComponent<DropKartu>();

            // Jika posisi mengenai script DropKartu ATAU kartu berada di dalam trigger zone
            if (drop != null || isInDropZone)
            {
                if (drop != null)
                {
                    drop.OnCardDropped(gameObject);
                }

                // Jalankan efek kartu secara instan
                ApplyCardEffect();

                // Hapus dari list HandManager2 agar kartu lain TIDAK MACET
                if (ownerHand != null)
                {
                    ownerHand.RemoveCardFromHand(this);
                }

                // Hancurkan kartu di tempat
                Destroy(gameObject);
                
                // BERHENTI DI SINI! Jangan biarkan kode ReturnCardToHand di bawah berjalan jika sukses drop
                return; 
            }
        }
        }
         private Vector3 GetMouseWorldPos()
        {
        Vector3 mousePos = Input.mousePosition;
        // Menyesuaikan kedalaman Z kartu terhadap kamera game
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DropKartu"))
            {
                isInDropZone = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("DropKartu"))
            {
                isInDropZone = false;
            }
        }

        private void ApplyCardEffect()
        {
            switch (cardType)
            {
                case CardType.Attack:
                {
                    int finalDamage = damage;
                    if (NextAttackIsBuffed)
                    {
                        finalDamage *= 2;
                        NextAttackIsBuffed = false;
                    }

                    
                    // TODO: ganti dengan sistem damage ke target/health kamu.
                    Debug.Log($"[Card] Attack: baseDamage={damage}, finalDamage={finalDamage} (buffed={NextAttackIsBuffed})");
                    break;
                }
                case CardType.Buff:
                {
                    
                    NextAttackIsBuffed = true;
                    Debug.Log($"[Card] Buff aktif: Attack berikutnya akan dikali 2. (damageBuff={damage})");
                    break;
                }
                case CardType.Debuff:
                {
                    // Placeholder sesuai kebutuhan.
                    Debug.Log($"[Card] Debuff dipakai. (damage={damage})");
                    break;
                }
            }
        }
        
        
    }
}
