using UnityEngine;

public class WeaponWithGun : Weapon
{
    [SerializeField] private GameObject bulletObject;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponTypes.Ranged;
        attackDamage = 25f;
    }

    protected override void Attack()
    {
        if (!CheckForNearbyEnemies())
            return;

        if (attackReload <= 0)
        {
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;

            CottonCandyBullet bulletInstance = CottonCandyBulletPool.Instance.bulletPool.Get();
            bulletInstance.attackDamage = attackDamage;
            bulletInstance.transform.position = transform.position;
            bulletInstance.SetDirection(direction);
            Wait();
        }
        else
            attackReload -= Time.deltaTime;
    }
}
