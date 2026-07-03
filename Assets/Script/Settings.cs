using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float currentVolume = 1f;
    [SerializeField] private float volumeStep = 0.1f;

    [Header("Reference")]
    [SerializeField] private AudioSource musicSource;

    [Header("UI")]
    [SerializeField] private Image volumeImage;

    private void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        musicSource.volume = currentVolume;
        UpdateVolumeUI();
    }

    public void IncreaseVolume()
    {
        currentVolume = Mathf.Clamp(currentVolume + volumeStep, 0f, maxVolume);

        musicSource.volume = currentVolume;
        UpdateVolumeUI();

        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
        PlayerPrefs.Save();
    }

    public void DecreaseVolume()
    {
        currentVolume = Mathf.Clamp(currentVolume - volumeStep, 0f, maxVolume);

        musicSource.volume = currentVolume;
        UpdateVolumeUI();

        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
        PlayerPrefs.Save();
    }

    private void UpdateVolumeUI()
    {
        if (volumeImage != null)
        {
            volumeImage.fillAmount = currentVolume / maxVolume;
        }
    }
}
