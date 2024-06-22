using UnityEngine;

public class WeaponWithGun : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] float enemyDetectionRadius = 5f;
    [SerializeField] float attackReload = 1f;
    [SerializeField] float rotationSpeed = 0.2f;

    [SerializeField] GameObject bulletObject;

    LayerMask enemyLayer;
    float timeReload;
    GameObject closestEnemy;

    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        timeReload = attackReload;

    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (CheckForNearbyEnemies())
        {
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

    bool CheckForNearbyEnemies()
    {
        bool isEnemyNearby = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyDetectionRadius, enemyLayer);
        if (colliders.Length <= 0)
            return isEnemyNearby;

        float minDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponentInParent<SpriteRenderer>().enabled)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = collider.gameObject;
                    isEnemyNearby = true;
                }
            }
        }
        if (isEnemyNearby)
            Aim(closestEnemy.GetComponent<Collider2D>());
        return isEnemyNearby;
    }

    void Aim(Collider2D enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        LeanTween.rotate(gameObject, rotation.eulerAngles, rotationSpeed);
    }

    void Wait()
    {
        attackReload = timeReload;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
    }
}
