using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float maxHealth = 100f;
    private float health;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;

    private void Start()
    {
        health = maxHealth;
        UpdateText();
    }

    public void TakeDamageFromPlayer(float damage)
    {
        if (health <= 0)
        {
            Die();
            return;
        }
        health -= damage;
        UpdateText();
    }

    private void Die()
    {
    }

    private void UpdateText()
    {
        playerHealthText.text = $"Health: {health}/{maxHealth}";
    }
}
