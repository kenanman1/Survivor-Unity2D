using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health = 100;

    [Header("Enemy Effects")]
    [SerializeField] private ParticleSystem particle;

    public void TakeDamageFromEnemy(float damage, bool isCritical = false)
    {
        health -= damage;
        if (health <= 0)
            Die();
        GetComponent<EnemyController>().DamageTextEffect(damage, isCritical);
    }

    private void Die()
    {
        ParticleSystem damageParticle = Instantiate(particle);
        damageParticle.transform.position = transform.position;
        var main = damageParticle.main;
        main.startColor = GetComponent<EnemyController>().GetColor();
        damageParticle.Play();

        GetComponent<EnemyController>().ReleaseEnemyToPool();
    }
}
