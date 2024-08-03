using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }
    public ObjectPool<Enemy> enemyPool;
    private Transform enemiesParent;

    [SerializeField] private Enemy enemyPrefab;

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

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.transform.SetParent(enemiesParent);
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.GetComponent<EnemyMovenment>().player = null;
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
