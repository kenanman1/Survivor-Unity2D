using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] float speed = 1f;

    Player player;

    void Follow()
    {
        if (player == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Update()
    {
        Follow();
    }

    public void SetFollow(Player player)
    {
        this.player = player;
    }
}
