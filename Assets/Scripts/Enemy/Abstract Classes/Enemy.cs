using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Abstract base class for all enemy types in the game.
/// </summary>
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovenment))]
[RequireComponent(typeof(EnemyController))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovenment enemyMovenment;

    [Header("Enemy Settings")]
    [SerializeField] protected float fadeTime = 0.6f;

    public static Action<Vector2> onDie;

    protected SpriteRenderer spawnIndicator;
    protected Player player;

    public abstract void ReleaseEnemyToPool();

    protected virtual void Awake()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        enemyMovenment = GetComponent<EnemyMovenment>();
    }
    protected void OnEnable()
    {
        // Reset state when the object is taken from the pool
        ResetState();
    }

    protected void ResetState()
    {
        spawnIndicator.enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartSpawnSequence();
    }

    protected void StartSpawnSequence()
    {
        spawnIndicator.color = GetComponent<EnemyController>().GetColor();
        LeanTween.alpha(spawnIndicator.gameObject, 0, fadeTime).setLoopPingPong(2).setEaseInOutSine().setOnComplete(StartFollow);
    }

    protected void SwitchRenderer()
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

    protected virtual void StartFollow()
    {
        GetComponent<Collider2D>().enabled = true;
        SwitchRenderer();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            return;

        enemyMovenment.SetFollow(player);
    }
}
