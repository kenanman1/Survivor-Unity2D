using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovenment))]
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamageFromPlayer(float damage)
    {
        playerHealth.TakeDamageFromPlayer(damage);
    }
}
