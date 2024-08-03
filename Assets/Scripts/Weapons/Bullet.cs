using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private float lifeTime = 5f;

    private bool hasHit = false; // Prevents multiple hits
    private Rigidbody2D rigidbody2D;
    private GameObject enemy;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (enemy != null)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            rigidbody2D.velocity = direction * speed;
        }
    }

    private void Update()
    {
        Wait();
    }

    public void Wait()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        if (collision.tag == "Enemy")
        {
            hasHit = true;
            collision.GetComponent<EnemyController>().TakeDamageFromEnemy(damage);
            Destroy(gameObject);
        }
    }
}
