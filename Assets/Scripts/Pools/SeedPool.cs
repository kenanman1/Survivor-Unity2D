using UnityEngine;
using UnityEngine.Pool;

public class SeedPool : MonoBehaviour
{
    [SerializeField] private RangeEnemyBullet seedPrefab;
    public static SeedPool Instance { get; private set; }
    public ObjectPool<RangeEnemyBullet> seedPool;
    private Transform seedsParent;

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
        seedPool = new ObjectPool<RangeEnemyBullet>(CreateSeed, OnGetSeed, OnReleaseSeed, OnDestroySeed, true, 100, 1000);
        CreateSeedsParent();
    }

    private void CreateSeedsParent()
    {
        seedsParent = new GameObject("Seeds").transform;
    }

    private RangeEnemyBullet CreateSeed()
    {
        RangeEnemyBullet seed = Instantiate(seedPrefab);
        seed.transform.SetParent(seedsParent);
        return seed;
    }

    private void OnGetSeed(RangeEnemyBullet seed)
    {
        seed.gameObject.SetActive(true);
    }

    private void OnReleaseSeed(RangeEnemyBullet seed)
    {
        seed.gameObject.SetActive(false);
    }

    private void OnDestroySeed(RangeEnemyBullet seed)
    {
        Destroy(seed.gameObject);
    }
}
