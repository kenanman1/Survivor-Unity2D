using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    GameObject follow;
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, follow.transform.position, speed * Time.deltaTime);
    }
}
