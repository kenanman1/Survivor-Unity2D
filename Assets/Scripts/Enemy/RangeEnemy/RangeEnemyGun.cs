using UnityEngine;

public class RangeEnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Player player;

    public void SetFollow(Player player)
    {
        this.player = player;
    }

    public void Shoot()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        RangeEnemyBullet bullet = bulletInstance.GetComponent<RangeEnemyBullet>();
        bullet.transform.right = direction;
        bullet.SetPlayer(player);
    }
}
