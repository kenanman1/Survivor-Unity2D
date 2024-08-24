using UnityEngine;
using UnityEngine.Pool;

public class CashPool : MonoBehaviour
{
    [SerializeField] private Cash cashPrefab;

    public static CashPool Instance { get; private set; }
    public ObjectPool<Cash> cashPool;
    private Transform cashParent;

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
        cashPool = new ObjectPool<Cash>(CreateCash, OnGetCash, OnReleaseCash, OnDestroyCash, true, 100, 1000);
        CreateCashParent();
    }

    private void CreateCashParent()
    {
        cashParent = new GameObject("Cash").transform;
    }

    private Cash CreateCash()
    {
        Cash cash = Instantiate(cashPrefab);
        cash.transform.SetParent(cashParent);
        return cash;
    }

    private void OnGetCash(Cash cash)
    {
        cash.gameObject.SetActive(true);
    }

    private void OnReleaseCash(Cash cash)
    {
        cash.gameObject.SetActive(false);
    }

    private void OnDestroyCash(Cash cash)
    {
        Destroy(cash.gameObject);
    }
}
