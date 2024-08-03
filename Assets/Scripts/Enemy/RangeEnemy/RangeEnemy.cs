using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyMovenment))]
[RequireComponent(typeof(RangeEnemyAttack))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyController))]
public class RangeEnemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovenment enemyMovenment;
    private RangeEnemyAttack rangeEnemyAttack;
    private RangeEnemyGun rangeEnemyGun;

    [Header("Enemy Settings")]
    [SerializeField] private float fadeTime = 0.6f;
    [SerializeField] private float rotationSpeed = 0.2f;

    private SpriteRenderer spawnIndicator;
    private Player player;

    private void Awake()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnIndicator = spriteRenderers.FirstOrDefault(sr => sr.gameObject.name == "Spawn Indicator");
        enemyMovenment = GetComponent<EnemyMovenment>();
        rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
        rangeEnemyGun = GetComponentInChildren<RangeEnemyGun>();
    }

    private void Update()
    {
        if (player != null)
            Aim();
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
        rangeEnemyAttack.SetFollow(player);
        rangeEnemyGun.SetFollow(player);
    }

    private void Aim()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        LeanTween.rotate(gameObject, rotation.eulerAngles, rotationSpeed);
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
