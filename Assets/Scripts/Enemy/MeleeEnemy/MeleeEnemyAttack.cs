using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{
    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 1f;

    protected override void Attack()
    {
        if (player == null || spawnIndicator.enabled)
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
}
