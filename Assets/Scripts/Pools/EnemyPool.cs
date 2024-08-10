using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }
    public ObjectPool<MeleeEnemy> enemyPool;
    private Transform enemiesParent;

    [SerializeField] private MeleeEnemy enemyPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        enemyPool = new(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, true, 100, 1000);
        CreateEnemiesParent();
    }

    private void CreateEnemiesParent()
    {
        enemiesParent = new GameObject("Enemies").transform;
    }

    private MeleeEnemy CreateEnemy()
    {
        MeleeEnemy enemy = Instantiate(enemyPrefab);
        enemy.transform.SetParent(enemiesParent);
        return enemy;
    }

    private void OnGetEnemy(MeleeEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }

    private void OnReleaseEnemy(MeleeEnemy enemy)
    {
        enemy.GetComponent<EnemyMovenment>().player = null;
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(MeleeEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
