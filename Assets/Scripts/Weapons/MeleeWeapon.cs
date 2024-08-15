using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float attackAnimationTime = 0.15f;

    private bool isAttacking = false;
    private Vector3 originalOffset;

    protected override void Start()
    {
        base.Start();
        originalOffset = transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();
        if (!isAttacking)
            transform.position = GetComponentInParent<Player>().transform.position + originalOffset;
    }

    protected override void Attack()
    {
        if (!CheckForNearbyEnemies())
            return;

        if (attackReload <= 0)
        {
            isAttacking = true;
            Vector3 originalPosition = transform.position;

            LeanTween.move(gameObject, closestEnemy.transform.position, attackAnimationTime).setOnComplete(() =>
            {
                closestEnemy.GetComponent<EnemyController>().TakeDamageFromEnemy(attackDamage);

                LeanTween.move(gameObject, GetComponentInParent<Player>().transform.position + originalOffset, attackAnimationTime).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
                {
                    isAttacking = false;
                });
            }).setEase(LeanTweenType.easeInOutSine);

            Wait();
        }
        else
            attackReload -= Time.deltaTime;
    }
}
