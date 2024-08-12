using UnityEngine;

public class WeaponWithGun : Weapon
{
    [SerializeField] private GameObject bulletObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        Attack();
    }

    protected override void Attack()
    {
        if (!CheckForNearbyEnemies())
            return;

        if (attackReload <= 0)
        {
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;

            CottonCandyBullet bulletInstance = CottonCandyBulletPool.Instance.bulletPool.Get();
            bulletInstance.transform.position = transform.position;
            bulletInstance.SetDirection(direction);
            Wait();
        }
        else
            attackReload -= Time.deltaTime;
    }
}
