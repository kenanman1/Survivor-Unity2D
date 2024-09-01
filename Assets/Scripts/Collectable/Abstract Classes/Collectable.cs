using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected float playerDetectionRadius = 3f;
    [SerializeField] protected float moveSpeedAnimation = 0.6f;

    protected bool isCollected = false;
    protected bool isMoving = false;
    protected Player player;

    public abstract void ReleaseCollectableToPool();

    protected void Start()
    {
        player = FindObjectOfType<Player>();
    }

    protected void Update()
    {
        DetectPlayer();

        if (isMoving)
            CheckDistanceToPlayer();
    }

    private void OnEnable()
    {
        isCollected = false;
        isMoving = false;
    }

    protected void DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player" && !isCollected)
            {
                isMoving = true;
                MoveTowardsPlayer();
            }
        }
    }

    protected void MoveTowardsPlayer()
    {
        LeanTween.move(gameObject, player.transform.position, moveSpeedAnimation).setOnComplete(() =>
        {
            isMoving = false;
        });
    }

    protected void CheckDistanceToPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > playerDetectionRadius)
            {
                LeanTween.cancel(gameObject);
                isMoving = false;
            }
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
