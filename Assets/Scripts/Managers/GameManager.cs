using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int count = 5;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        while (count > 0)
        {
            SpawnEnemy();
            count--;
        }
    }

    private void SpawnEnemy()
    {
        FindObjectOfType<EnemyPool>().enemyPool.Get();
    }
}
