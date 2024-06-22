using UnityEngine;

/// <summary>
/// The EnemyController class is responsible for managing the enemy's state.
/// </summary>
public class EnemyController : MonoBehaviour
{
    EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public void TakeDamageFromEnemy(float damage)
    {
        enemyHealth.TakeDamageFromEnemy(damage);
    }
}
