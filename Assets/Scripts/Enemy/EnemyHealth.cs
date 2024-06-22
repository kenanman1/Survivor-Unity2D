using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] TextMeshPro enemyHealthText;
    [Header("Enemy Settings")]
    public float health = 100;

    [Header("Enemy Effects")]
    [SerializeField] ParticleSystem particle;

    private void Start()
    {
        enemyHealthText.text = health.ToString();
    }

    public void TakeDamageFromEnemy(float damage)
    {
        health -= damage;
        enemyHealthText.text = health.ToString();
        if (health <= 0)
            Die();
    }

    void Die()
    {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
        var main = particleInstance.main;
        main.startColor = GetComponent<Enemy>().GetColor();
        particleInstance.Play();

        Destroy(gameObject);
    }
}
