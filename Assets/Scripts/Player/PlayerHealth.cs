using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float maxHealth = 100f;

    public float health;

    private void Start()
    {
        health = maxHealth;
        UpdateText();
    }

    public void TakeDamageFromPlayer(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnPlayerDeath();
            return;
        }
        UpdateText();
    }

    private void OnPlayerDeath()
    {
        GetComponent<PlayerController>().OnPlayerDeath();
        LeanTween.rotate(gameObject, new Vector3(0, 0, -90), 1f);
    }

    private void UpdateText()
    {
        UIManager.Instance.UpdatePlayerText(health, maxHealth);
    }
}
