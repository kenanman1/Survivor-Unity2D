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
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            RangeEnemyBullet bulletInstance = SeedPool.Instance.seedPool.Get();
            bulletInstance.transform.position = transform.position;
            bulletInstance.transform.right = direction;
            bulletInstance.SetDirection(direction);
        }
    }
}
