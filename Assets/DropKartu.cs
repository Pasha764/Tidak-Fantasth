using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKartu : MonoBehaviour
{
    public void OnCardDropped(GameObject card)
    {
        Debug.Log(card.name + " berhasil di-drop!");

        // Menempelkan kartu ke DropKartu
        card.transform.position = transform.position;

        // Contoh kalau ingin mengaktifkan script tertentu di kartu
        // card.GetComponent<Card>().AktifkanKartu();
    }
}
