using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelPaused;
    public GameObject buttonPause;
    public GameObject buttonResume;
    public GameObject buttonRestart;
    public GameObject buttonMainMenu;
    public static bool isGamePaused = false;

    public CanvasGroup gameplayCanvasGroup;  
    void Start()
    {
        AllButtonOff();
        buttonPause.SetActive(true);

        if (gameplayCanvasGroup != null)
        {
            gameplayCanvasGroup.interactable = true;
            gameplayCanvasGroup.blocksRaycasts = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AllButtonOff()
    {
        buttonPause.SetActive(false);
        buttonRestart.SetActive(false);
        buttonResume.SetActive(false);
        buttonMainMenu.SetActive(false);
    }
    void AllButtonOn()
    {
        buttonPause.SetActive(true);
        buttonRestart.SetActive(true);
        buttonResume.SetActive(true);
        buttonMainMenu.SetActive(true);
    }

    public void Paused()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        panelPaused.SetActive(true);
        AllButtonOn();
        buttonPause.SetActive(false);

        if (gameplayCanvasGroup != null)
        {
            gameplayCanvasGroup.interactable = false; 
            gameplayCanvasGroup.blocksRaycasts = false; 
        }
    }

    public void Resume()
    {
        isGamePaused = false;
        AllButtonOff();
        panelPaused.SetActive(false);
        buttonPause.SetActive(true);
        Time.timeScale = 1f;

        if (gameplayCanvasGroup != null)
        {
            gameplayCanvasGroup.interactable = true;
            gameplayCanvasGroup.blocksRaycasts = true;
        }
    }

    public void Restart()
    {
        AllButtonOff();
        buttonPause.SetActive(true);
        Time.timeScale = 1f;

        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentScene);

    }

    public void MainMenu()
    {
        AllButtonOff();
        SceneManager.LoadScene(0);
    }
    
}
