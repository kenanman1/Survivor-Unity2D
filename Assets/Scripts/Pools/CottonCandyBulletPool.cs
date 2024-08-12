using UnityEngine;
using UnityEngine.Pool;  

public class CottonCandyBulletPool : MonoBehaviour
{
    [SerializeField] private CottonCandyBullet bulletPrefab;
    public static CottonCandyBulletPool Instance { get; private set; }
    public ObjectPool<CottonCandyBullet> bulletPool;
    private Transform bulletsParent;

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
        bulletPool = new ObjectPool<CottonCandyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true, 100, 1000);
        CreateBulletsParent();
    }

    private void CreateBulletsParent()
    {
        bulletsParent = new GameObject("CottonCandyBullets").transform;
    }

    private CottonCandyBullet CreateBullet()
    {
        CottonCandyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.SetParent(bulletsParent);
        return bullet;
    }

    private void OnGetBullet(CottonCandyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(CottonCandyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(CottonCandyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
