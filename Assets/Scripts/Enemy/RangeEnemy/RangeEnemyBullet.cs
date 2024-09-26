using System.Collections;
using UnityEngine;

public class RangeEnemyBullet : Bullet
{
    public float damage;

    protected override IEnumerator AutoReturnToPool(float delay)
    {
        yield return base.AutoReturnToPool(delay);
        SeedPool.Instance.seedPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            hasHit = true;
            collision.GetComponent<PlayerController>().TakeDamageFromPlayer(damage);

            SeedPool.Instance.seedPool.Release(this);
        }
    }
}
