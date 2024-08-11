using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] protected float enemyDetectionRadius = 5f;
    [SerializeField] protected float attackReload = 1f;
    [SerializeField] protected float rotationSpeed = 0.2f;
    [SerializeField] protected float attackDamage = 1f;

    protected LayerMask enemyLayer;
    protected float timeReload;
    protected GameObject closestEnemy;

    protected abstract void Attack();

    protected virtual void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        timeReload = attackReload;
    }

    protected virtual void Update()
    {
        Attack();
    }

    protected bool CheckForNearbyEnemies()
    {
        bool isEnemyNearby = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyDetectionRadius, enemyLayer);
        if (colliders.Length <= 0)
            return isEnemyNearby;

        float minDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = collider.gameObject;
                isEnemyNearby = true;
            }
        }
        if (isEnemyNearby)
            Aim(closestEnemy.GetComponent<Collider2D>());
        return isEnemyNearby;
    }

    protected void Aim(Collider2D enemy)
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        LeanTween.rotate(gameObject, rotation.eulerAngles, rotationSpeed);
    }

    protected void Wait()
    {
        attackReload = timeReload;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
    }
}
