using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health = 100;

    [Header("Enemy Effects")]
    [SerializeField] ParticleSystem particle;

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

    void Die()
    {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
        var main = particleInstance.main;
        main.startColor = GetComponent<EnemyController>().GetColor();
        particleInstance.Play();

        Destroy(gameObject);
    }
}
