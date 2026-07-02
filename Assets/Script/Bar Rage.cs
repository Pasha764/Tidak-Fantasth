using UnityEngine;
using UnityEngine.UI;

public class BarRage : MonoBehaviour
{
    public static BarRage Instance;

    [Header("Rage Settings")]
    [SerializeField] private float maxRage = 100f;
    [SerializeField] private float currentRage = 0f;

    [Header("UI")]
    [SerializeField] private Image rageImage;
    public GameObject winUi;
    public GameObject loseUi;

    public float CurrentRage => currentRage;
    public float MaxRage => maxRage;
    public RoundManager roundManager;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        winUi.SetActive(false);
    }

    public void SetRage(float value)
    {
        currentRage = Mathf.Clamp(value, 0f, maxRage);
        UpdateUI();
        Check();
    }

    public void AddRage(float amount)
    {
        currentRage = Mathf.Clamp(currentRage + amount, 0f, maxRage);
        UpdateUI();
        Check();
    }

    public bool ConsumeRage(float amount)
    {
        if (currentRage < amount)
            return false;

        currentRage -= amount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        if (rageImage != null)
        {
            rageImage.fillAmount = currentRage / maxRage;
        }
    }

    public void CheckWin()
    {
        
            Debug.Log("PLAYER WIN!");
            winUi.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            
    }
    public void CheckLose()
    {
        
            Debug.Log("PLAYER LOSE!");
            loseUi.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            
    }


    public void Check()
    {
        
        int roundCurrent = roundManager.currentRound;
        int roundMax = roundManager.maxRound;

        if(roundCurrent == roundMax)
        {
            if (currentRage < maxRage)
            {
                CheckLose();
            }
            if (currentRage == maxRage)
            {
                CheckWin();
            }
            
        }
        else
        {
            if (currentRage == maxRage)
            {
                CheckWin();
            }
        }

        
    }
}

