using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] float speed = 1f;
    [SerializeField] float playerDetection = 1f;
    [SerializeField] float fadeTime = 0.6f;

    [Header("Enemy Effects")]
    [SerializeField] ParticleSystem particle;

    SpriteRenderer spawnIndicator;
    GameObject follow;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        spawnIndicator.color = gameObject.name switch
        {
            var name when name.Contains("purple") => new Color(0.5f, 0, 0.5f),
            var name when name.Contains("yellow") => Color.yellow,
            var name when name.Contains("red") => Color.red,
            _ => Color.white,
        };
        LeanTween.alpha(spawnIndicator.gameObject, 0, fadeTime).setLoopPingPong(2).setEaseInOutSine().setOnComplete(StartFollow);
    }

    void StartFollow()
    {
        spawnIndicator.enabled = false;
        GetComponent<SpriteRenderer>().enabled = true;

        follow = GameObject.FindGameObjectWithTag("Player");
        if (follow == null)
            Destroy(gameObject);
    }

    void Update()
    {
        Follow();
        Attack();
    }

    void Follow()
    {
        if (follow == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, follow.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (follow == null)
            return;

        if (Vector3.Distance(transform.position, follow.transform.position) < playerDetection)
        {
            Die();
        }
    }

    void Die()
    {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
        var main = particleInstance.main;
        main.startColor = gameObject.name switch
        {
            var name when name.Contains("purple") => new Color(0.5f, 0, 0.5f),
            var name when name.Contains("yellow") => Color.yellow,
            var name when name.Contains("red") => Color.red,
            _ => Color.white,
        };
        particleInstance.Play();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }
}
