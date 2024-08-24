using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI candyText;
    [SerializeField] private TextMeshProUGUI cashText;

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
}
