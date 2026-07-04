using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource bgm;
    [SerializeField] private AudioClip sfxButton;
    [SerializeField] private AudioClip sfxKesel;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxDrawCard;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TurnOnSFXButton()
    {
        AudioSource.PlayClipAtPoint(sfxButton, Camera.main.transform.position);
    }

    public void TurnOnSFXKesel()
    {
        AudioSource.PlayClipAtPoint(sfxKesel, Camera.main.transform.position);
    }
    public void TurnOnSFXWIN()
    {
        TurnOffBgm();
        AudioSource.PlayClipAtPoint(sfxWin, Camera.main.transform.position);
    }
    public void TurnOnSFXDrawCard()
    {
        AudioSource.PlayClipAtPoint(sfxDrawCard, Camera.main.transform.position);
    }
    public void TurnOffBgm()
    {
        bgm.Stop();
    }
}
