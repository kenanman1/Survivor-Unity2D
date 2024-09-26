using UnityEngine;

/// <summary>
/// The EnemyController class is responsible for managing the enemy's state.
/// </summary>
public class EnemyController : MonoBehaviour
{
    private void Start()
    {
        PlayerController.OnPlayerDeathEvent += UnfollowPlayer;
    }

    public void TakeDamageFromEnemy(float damage, bool isCritical = false)
    {
        GetComponent<EnemyHealth>().TakeDamageFromEnemy(damage, isCritical);
    }

    public void DamageTextEffect(float damage, bool isCritical = false)
    {
        GetComponentInChildren<DamageTextEffect>().AnimateDamageText(damage, isCritical);
    }

    public Color GetColor()
    {
        return gameObject.name switch
        {
            var name when name.Contains("purple") => new Color(0.5f, 0, 0.5f),
            var name when name.Contains("yellow") => Color.yellow,
            var name when name.Contains("red") => Color.red,
            var name when name.Contains("green") => Color.green,
            _ => Color.white,
        };
    }

    internal void ReleaseEnemyToPool()
    {
        var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            enemy.ReleaseEnemyToPool();
        }
    }

    private void UnfollowPlayer()
    {
        foreach (IPlayerDependent playerDependent in GetComponentsInChildren<IPlayerDependent>())
        {
            playerDependent.ClearPlayerReference();
        }
    }
}
