using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 1f;

    public Player player;

    private void Follow()
    {
        if (player == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Update()
    {
        Follow();
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }
}
