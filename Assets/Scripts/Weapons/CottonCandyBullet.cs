using System.Collections;
using UnityEngine;

public class CottonCandyBullet : Bullet
{
    protected override IEnumerator AutoReturnToPool(float delay)
    {
        yield return base.AutoReturnToPool(delay);
        CottonCandyBulletPool.Instance.bulletPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        if (collision.tag == "Enemy")
        {
            StopAllCoroutines();
            hasHit = true;
            collision.GetComponent<EnemyController>().TakeDamageFromEnemy(damage);

            CottonCandyBulletPool.Instance.bulletPool.Release(this);
        }
    }
}
