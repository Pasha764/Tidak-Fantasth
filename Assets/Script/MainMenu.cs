using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    void Start()
    {
        settingsMenu.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SoundManager.Instance.TurnOnSFXButton();
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        SoundManager.Instance.TurnOnSFXButton();
        Application.Quit();
    }

    public void CloseSettings()
    {
        SoundManager.Instance.TurnOnSFXButton();
        settingsMenu.SetActive(false);
    }
}
