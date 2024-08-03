using UnityEngine;

/// <summary>
/// The PlayerController class is responsible for managing the player's state.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamageFromPlayer(float damage)
    {
        playerHealth.TakeDamageFromPlayer(damage);
    }
}
