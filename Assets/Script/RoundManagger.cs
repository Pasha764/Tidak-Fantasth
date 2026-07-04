using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Round")]
    public int currentRound = 1;
    public int maxRound;

    [Header("Reference")]
    [SerializeField] private HandManager handManager;
    public GameObject gameOverUi;

    [Header("UI")]
    public Text roundText;

    public BarRage rageBar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateRoundUI();
        SoundManager.Instance.TurnOnSFXDrawCard();

        // Draw 3 kartu saat game dimulai
        handManager.StartNewRound();
        
        gameOverUi.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(roundText);
            Debug.Log(roundText.text);
            Debug.Log(roundText.gameObject.activeInHierarchy);
        }

    }
    public void NextRound()
    {
        currentRound++;

        if (currentRound == maxRound)
        {
            rageBar.CheckLose();
        }

        UpdateRoundUI();

        handManager.StartNewRound();
        SoundManager.Instance.TurnOnSFXDrawCard();
    }

    private void UpdateRoundUI()
    {
        roundText.text = $"Round {currentRound}/{maxRound}";
    }

    //private void GameOver()
    //{
    //    Debug.Log("GAME OVER");

    //    gameOverUi.SetActive(true);
    //    Time.timeScale = 0;
    //}
}
