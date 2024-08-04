using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangeEnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(AutoReturnToPool(lifeTime));
    }

    private IEnumerator AutoReturnToPool(float delay)
    {
        yield return new WaitForSeconds(delay);
        SeedPool.Instance.seedPool.Release(this);
    }

    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamageFromPlayer(damage);
            SeedPool.Instance.seedPool.Release(this);
        }
    }
}
