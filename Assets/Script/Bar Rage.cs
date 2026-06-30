using UnityEngine;
using UnityEngine.UI;

public class BarRage : MonoBehaviour
{
    [Header("Rage Settings")]
    [SerializeField] private float maxRage = 100f;
    [SerializeField] private float currentRage = 0f;

    [Header("UI")]
    [SerializeField] private Image rageImage;

    public float CurrentRage => currentRage;
    public float MaxRage => maxRage;

    private void Start()
    {
        ClampAndUpdateUI();
    }

    private void Update()
    {
        // Hanya untuk memastikan UI konsisten jika ada script lain yang mengubah nilai tanpa memanggil method.
        ClampAndUpdateUI();
    }

    public void SetRage(float value)
    {
        currentRage = value;
        ClampAndUpdateUI();
    }

    public void AddRage(float amount)
    {
        currentRage += amount;
        ClampAndUpdateUI();
    }

    public bool ConsumeRage(float amount)
    {
        if (currentRage < amount) return false;
        currentRage -= amount;
        ClampAndUpdateUI();
        return true;
    }

    public void ClampAndUpdateUI()
    {
        if (maxRage <= 0f)
        {
            currentRage = 0f;
            if (rageImage != null) rageImage.fillAmount = 0f;
            return;
        }

        currentRage = Mathf.Clamp(currentRage, 0f, maxRage);

        if (rageImage != null)
        {
            rageImage.fillAmount = currentRage / maxRage;
        }
    }
}

