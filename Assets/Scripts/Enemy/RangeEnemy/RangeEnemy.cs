using UnityEngine;

[RequireComponent(typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{
    [Header("Components")]
    private RangeEnemyAttack rangeEnemyAttack;
    private RangeEnemyGun rangeEnemyGun;

    [Header("Enemy Settings")]
    [SerializeField] private float rotationSpeed = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
        rangeEnemyGun = GetComponentInChildren<RangeEnemyGun>();
    }

    private void Update()
    {
        if (player != null)
            Aim();
    }

    protected override void StartFollow()
    {
        base.StartFollow();
        rangeEnemyAttack.SetFollow(player);
        rangeEnemyGun.SetFollow(player);
    }

    private void Aim()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        LeanTween.rotate(gameObject, rotation.eulerAngles, rotationSpeed);
    }

    public override void ReleaseEnemyToPool()
    {
        if (gameObject.activeSelf)
            RangeEnemyPool.Instance.enemyPool.Release(this);
    }
}
