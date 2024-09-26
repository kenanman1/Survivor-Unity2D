using System.Linq;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour, IPlayerDependent
{
    [Header("Attack Settings")]
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected float playerDetection = 1f;

    public Player player;

    protected float attackTimer;
    protected SpriteRenderer spawnIndicator;

    protected abstract void Attack();

    protected virtual void Start()
    {
        attackTimer = attackDelay;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
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

    public void ClearPlayerReference()
    {
        player = null;
    }
}
