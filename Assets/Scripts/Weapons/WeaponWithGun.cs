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
            GameObject bulletInstance = Instantiate(bulletObject, transform.position, Quaternion.identity);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.SetEnemy(closestEnemy);
            Wait();
        }
        else
            attackReload -= Time.deltaTime;

    }
}
