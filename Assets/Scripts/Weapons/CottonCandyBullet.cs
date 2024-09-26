﻿using System.Collections;
using UnityEngine;

public class CottonCandyBullet : Bullet
{
    [Header("Bullet Settings")]
    [SerializeField] private float criticalChance = 40;
    public float attackDamage;

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
            bool isCritical = Random.Range(0, 100) < criticalChance;
            float newDamage = isCritical ? attackDamage * 2 : attackDamage;
            collision.GetComponent<EnemyHealth>().TakeDamageFromEnemy(newDamage, isCritical);

            CottonCandyBulletPool.Instance.bulletPool.Release(this);
        }
    }
}
