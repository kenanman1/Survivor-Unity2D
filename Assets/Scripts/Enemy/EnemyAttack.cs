using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] float attackDamage = 1f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float playerDetection = 1f;

    Player player;
    float attackTimer;

    void Start()
    {
        attackTimer = attackDelay;
    }

    void Update()
    {
        Attack();
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }

    void Attack()
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

    void Wait()
    {
        attackDelay = attackTimer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }
}
