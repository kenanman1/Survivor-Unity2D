using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovenment))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyController))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    EnemyMovenment enemyMovenment;
    EnemyAttack enemyAttack;

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
        spawnIndicator.color = GetComponent<EnemyController>().GetColor();
        LeanTween.alpha(spawnIndicator.gameObject, 0, fadeTime).setLoopPingPong(2).setEaseInOutSine().setOnComplete(StartFollow);
    }

    void StartFollow()
    {
        GetComponent<Collider2D>().enabled = true;
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
}
