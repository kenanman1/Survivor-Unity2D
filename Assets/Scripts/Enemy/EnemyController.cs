using UnityEngine;

/// <summary>
/// The EnemyController class is responsible for managing the enemy's state.
/// </summary>
public class EnemyController : MonoBehaviour
{
    void Start()
    {
    }

    public void TakeDamageFromEnemy(float damage)
    {
        GetComponent<EnemyHealth>().TakeDamageFromEnemy(damage);
    }

    public void DamageTextEffect(float damage)
    {
        GetComponentInChildren<DamageTextEffect>().AnimateDamageText(damage);
    }

    public Color GetColor()
    {
        return gameObject.name switch
        {
            var name when name.Contains("purple") => new Color(0.5f, 0, 0.5f),
            var name when name.Contains("yellow") => Color.yellow,
            var name when name.Contains("red") => Color.red,
            _ => Color.white,
        };
    }
}
