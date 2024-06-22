using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovenment))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    EnemyMovenment enemyMovenment;
    EnemyAttack enemyAttack;
    EnemyHealth enemyHealth;

    [Header("Enemy Settings")]
    [SerializeField] float fadeTime = 0.6f;

    SpriteRenderer spawnIndicator;
    Player player;

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

    public Color GetColor()
    {
        return gameObject.name switch
        {
            var name when name.Contains("purple") => new Color(0.5f, 0, 0.5f),
            var name when name.Contains("yellow") => Color.yellow,
            var name when name.Contains("red") => Color.red,
            _ => Color.white,
        };
    }
}
