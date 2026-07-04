using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sfx : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audioSource;
    
    [Header("Sound Clips")]
    public AudioClip clickSound;


    // Suara muncul saat tombol dipencet
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
