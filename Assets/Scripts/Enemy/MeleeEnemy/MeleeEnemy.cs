using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public class MeleeEnemy : Enemy
{
    [Header("Components")]
    private MeleeEnemyAttack enemyAttack;

    protected override void Awake()
    {
        base.Awake();
        enemyAttack = GetComponent<MeleeEnemyAttack>();
    }

    public void Update()
    {
        GetComponent<EnemyMovenment>().Follow();
    }

    protected override void StartFollow()
    { 
        base.StartFollow();
        enemyAttack.SetFollow(player);
    }

    public override void ReleaseEnemyToPool()
    {
        EnemyPool.Instance.enemyPool.Release(this);
    }
}