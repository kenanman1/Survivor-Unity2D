using UnityEngine;
using UnityEngine.Pool;

public class CandyPool : MonoBehaviour
{
    [SerializeField] private Candy candyPrefab;

    public static CandyPool Instance { get; private set; }
    public ObjectPool<Candy> candyPool;
    private Transform candiesParent;

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
        candyPool = new ObjectPool<Candy>(CreateCandy, OnGetCandy, OnReleaseCandy, OnDestroyCandy, true, 100, 1000);
        CreateCandiesParent();
    }

    private void CreateCandiesParent()
    {
        candiesParent = new GameObject("Candies").transform;
    }

    private Candy CreateCandy()
    {
        Candy candy = Instantiate(candyPrefab);
        candy.transform.SetParent(candiesParent);
        return candy;
    }

    private void OnGetCandy(Candy candy)
    {
        candy.gameObject.SetActive(true);
    }

    private void OnReleaseCandy(Candy candy)
    {
        candy.gameObject.SetActive(false);
    }

    private void OnDestroyCandy(Candy candy)
    {
        Destroy(candy.gameObject);
    }
}
