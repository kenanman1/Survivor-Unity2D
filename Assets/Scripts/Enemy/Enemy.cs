using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovenment))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyController))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovenment enemyMovenment;
    private EnemyAttack enemyAttack;

    [Header("Enemy Settings")]
    [SerializeField] private float fadeTime = 0.6f;

    private SpriteRenderer spawnIndicator;
    private Player player;

    private void Awake()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        enemyMovenment = GetComponent<EnemyMovenment>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        // Reset state when the object is taken from the pool
        ResetState();
    }

    private void ResetState()
    {
        spawnIndicator.enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartSpawnSequence();
    }

    private void StartSpawnSequence()
    {
        spawnIndicator.color = GetComponent<EnemyController>().GetColor();
        LeanTween.alpha(spawnIndicator.gameObject, 0, fadeTime).setLoopPingPong(2).setEaseInOutSine().setOnComplete(StartFollow);
    }

    private void StartFollow()
    {
        GetComponent<Collider2D>().enabled = true;
        SwitchRenderer();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            return;

        enemyMovenment.SetFollow(player);
        enemyAttack.SetFollow(player);
    }

    private void SwitchRenderer()
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