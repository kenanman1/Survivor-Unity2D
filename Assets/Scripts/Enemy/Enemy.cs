using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovenment))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    EnemyMovenment enemyMovenment;
    EnemyAttack enemyAttack;

    [Header("Enemy Settings")]
    [SerializeField] float playerDetection = 1f;
    [SerializeField] float fadeTime = 0.6f;

    [Header("Enemy Effects")]
    [SerializeField] ParticleSystem particle;

    SpriteRenderer spawnIndicator;
    GameObject follow;

    void Start()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        enemyMovenment = GetComponent<EnemyMovenment>();
        enemyAttack = GetComponent<EnemyAttack>();
        StartSpawnSequence();
    }

    void StartSpawnSequence()
    {
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
        SwitchRenderer();
        follow = GameObject.FindGameObjectWithTag("Player");
        if (follow == null)
            Destroy(gameObject);
        enemyMovenment.SetFollow(follow);
        enemyAttack.SetFollow(follow);
    }

    void SwitchRenderer()
    {
        if (spawnIndicator.enabled == false)
        {
            spawnIndicator.enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            spawnIndicator.enabled = false;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Update()
    {

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
