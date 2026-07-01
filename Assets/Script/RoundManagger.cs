using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Round")]
    public int currentRound = 1;
    public int maxRound = 10;

    [Header("Reference")]
    [SerializeField] private HandManager handManager;

    [Header("UI")]
    public Text roundText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateRoundUI();

        // Draw 3 kartu saat game dimulai
        handManager.StartNewRound();
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

        if (currentRound > maxRound)
        {
            GameOver();
            return;
        }

        UpdateRoundUI();

        handManager.StartNewRound();
    }

    private void UpdateRoundUI()
    {
        roundText.text = $"Round {currentRound}/{maxRound}";
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");

        // Contoh
        // gameOverPanel.SetActive(true);
         Time.timeScale = 0;
    }
}
