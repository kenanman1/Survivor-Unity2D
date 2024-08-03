using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health = 100;

    [Header("Enemy Effects")]
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {

    }

    public void TakeDamageFromEnemy(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        GetComponent<EnemyController>().DamageTextEffect(damage);
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
