using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float playerDetection = 10f;
    [SerializeField] private float scaleAnimation = 0.3f;
    [SerializeField] private float scaleAnimationTime = 0.2f;

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
