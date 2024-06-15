using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] float speed = 1f;

    GameObject follow;

    void Follow()
    {
        if (follow == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, follow.transform.position, speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    public void SetFollow(GameObject follow)
    {
        this.follow = follow;
    }
}
