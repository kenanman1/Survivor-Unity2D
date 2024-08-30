using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI candyText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider levelSlider;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerText(float health, float maxHealth)
    {
        playerHealthText.text = $"Health: {health}/{maxHealth}";
    }

    public void UpdateCandyText(float candy)
    {
        candyText.text = $"Collected: {candy}";
    }

    internal void UpdateCashText(float cashCount)
    {
        cashText.text = $"Collected: {cashCount}";
    }

    public void UpdateLevelSlider(float count)
    {
        levelSlider.value = count;
    }

    public void RestoreLevelSlider(float newMax)
    {
        float newMin = levelSlider.value;
        levelSlider.value = 0;
        levelSlider.minValue = newMin;
        levelSlider.maxValue = newMax;
    }

    public void UpdateLevelText(float level)
    {
        levelText.text = $"Level: {level}";
    }
}
