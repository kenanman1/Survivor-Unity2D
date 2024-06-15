using System.Linq;
using UnityEngine;

enum ColorType
{
    Purple,
    Yellow,
    Red,
    White
}

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
    Player player;
    ColorType colorType;

    void Start()
    {
        colorType = gameObject.name switch
        {
            var name when name.Contains("purple") => ColorType.Purple,
            var name when name.Contains("yellow") => ColorType.Yellow,
            var name when name.Contains("red") => ColorType.Red,
            _ => ColorType.White,
        };
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        enemyMovenment = GetComponent<EnemyMovenment>();
        enemyAttack = GetComponent<EnemyAttack>();
        StartSpawnSequence();
    }

    void StartSpawnSequence()
    {
        spawnIndicator.color = GetColor();
        LeanTween.alpha(spawnIndicator.gameObject, 0, fadeTime).setLoopPingPong(2).setEaseInOutSine().setOnComplete(StartFollow);
    }

    void StartFollow()
    {
        SwitchRenderer();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            Destroy(gameObject);
        enemyMovenment.SetFollow(player);
        enemyAttack.SetFollow(player);
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
        main.startColor = GetColor();

        particleInstance.Play();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }

    Color GetColor()
    {
        return colorType switch
        {
            ColorType.Red => Color.red,
            ColorType.Yellow => Color.yellow,
            ColorType.Purple => new Color(0.5f, 0, 0.5f),
            _ => Color.white,
        };
    }
}
