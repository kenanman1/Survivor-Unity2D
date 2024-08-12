using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float damage = 25f;
    [SerializeField] protected float lifeTime = 5f;

    protected bool hasHit = false; // Prevents multiple hits
    protected Rigidbody2D rigidbody2D;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(AutoReturnToPool(lifeTime));
        hasHit = false;
    }

    public void SetDirection(Vector3 direction)
    {
        rigidbody2D.velocity = direction * speed;
    }

    protected virtual IEnumerator AutoReturnToPool(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
