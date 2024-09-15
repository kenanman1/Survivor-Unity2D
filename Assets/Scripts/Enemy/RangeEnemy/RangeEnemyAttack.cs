using UnityEngine;

public class RangeEnemyAttack : EnemyAttack
{
    [Header("Attack Settings")]
    [SerializeField] private float scaleAnimation = 0.3f;
    [SerializeField] private float scaleAnimationTime = 0.2f;

    protected override void Attack()
    {
        if (player == null || spawnIndicator.enabled)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) < playerDetection)
        {
            if (attackDelay <= 0)
            {
                GetComponentInChildren<RangeEnemyGun>().Shoot();
                LeanTween.moveLocal(gameObject, transform.localPosition + Vector3.up * scaleAnimation, scaleAnimationTime).setLoopPingPong(1);
                Wait();
            }
            else
                attackDelay -= Time.deltaTime;
        }
        else
        {
            GetComponent<EnemyMovenment>().Follow();
        }
    }
}
