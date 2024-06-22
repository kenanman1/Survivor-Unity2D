using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float damage = 25f;

    bool hasHit = false;
    Rigidbody2D rigidbody2D;
    GameObject enemy;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }

    void Update()
    {
        if (enemy != null)
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        else
            Destroy(gameObject);
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
