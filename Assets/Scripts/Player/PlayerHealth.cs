using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float maxHealth = 100f;
    private float health;

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
        UIManager.Instance.UpdatePlayerText(health, maxHealth);
    }
}
