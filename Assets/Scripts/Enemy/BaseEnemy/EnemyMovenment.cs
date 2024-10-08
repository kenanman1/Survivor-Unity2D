using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 1f;

    public Player player;

    public void Follow()
    {
        if (player == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }
}
