using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float playerDetection = 1f;

    private Player player;
    private float attackTimer;

    private void Start()
    {
        attackTimer = attackDelay;
    }

    private void Update()
    {
        Attack();
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }

    private void Attack()
    {
        if (player == null)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) < playerDetection)
        {
            if (attackDelay <= 0)
            {
                player.GetComponent<PlayerController>().TakeDamageFromPlayer(attackDamage);
                Wait();
            }
            else
                attackDelay -= Time.deltaTime;
        }
    }

    private void Wait()
    {
        attackDelay = attackTimer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }
}
