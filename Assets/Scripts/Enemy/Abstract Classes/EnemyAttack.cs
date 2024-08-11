using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected float playerDetection = 1f;

    protected Player player;
    protected float attackTimer;

    protected abstract void Attack();

    protected virtual void Start()
    {
        attackTimer = attackDelay;
    }

    protected virtual void Update()
    {
        Attack();
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }

    protected void Wait()
    {
        attackDelay = attackTimer;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }
}
