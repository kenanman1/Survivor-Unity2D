using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangeEnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float lifeTime = 5f;

    private Player player;

    private void Start()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamageFromPlayer(damage);
            Destroy(gameObject);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
