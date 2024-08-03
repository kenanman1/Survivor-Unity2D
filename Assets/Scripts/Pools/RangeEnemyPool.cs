using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyPool : MonoBehaviour
{
    public static RangeEnemyPool Instance { get; private set; }
    public ObjectPool<RangeEnemy> enemyPool;
    private Transform enemiesParent;

    [SerializeField] private RangeEnemy enemyPrefab;

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
        enemiesParent = new GameObject("Range_Enemies").transform;
    }

    private RangeEnemy CreateEnemy()
    {
        RangeEnemy enemy = Instantiate(enemyPrefab);
        enemy.transform.SetParent(enemiesParent);
        return enemy;
    }

    private void OnGetEnemy(RangeEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }

    private void OnReleaseEnemy(RangeEnemy enemy)
    {
        enemy.GetComponent<EnemyMovenment>().player = null;
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(RangeEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
