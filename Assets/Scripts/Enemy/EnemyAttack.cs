using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] float attackDamage = 1f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float playerDetection = 1f;

    GameObject follow;
    float attackTimer;

    void Start()
    {
        attackTimer = attackDelay;
    }

    void Update()
    {
        Attack();
    }

    public void SetFollow(GameObject follow)
    {
        this.follow = follow;
    }

    void Attack()
    {
        if (follow == null)
            return;

        if (Vector3.Distance(transform.position, follow.transform.position) < playerDetection)
        {
            if (attackDelay <= 0)
            {
                print("Attack");
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
}
