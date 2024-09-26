using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public static WaveManager instance;

    [SerializeField] private int maxWave = 10;
    private int currentWave = 0;

    private Wave[] waves;
    private float timeBetweenWaves = 10f;

    private float timeBetweenWavesIncrease = 1.05f;
    private float rangeEnemyIncrease = 1.1f;
    private float meleeEnemyIncrease = 1.4f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartWaves();
    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    public void StopWaves()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < maxWave)
        {
            currentWave++;
            UIManager.Instance.UpdateWaveText(currentWave, maxWave);

            Wave wave = GenerateWave(currentWave);
            yield return StartCoroutine(SpawnWave(wave));

            if (currentWave < maxWave)
            {
                UIManager.Instance.UpdateWaveCountdownText(timeBetweenWaves);
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            timeBetweenWaves = timeBetweenWaves * timeBetweenWavesIncrease;
        }
    }

    private Wave GenerateWave(int waveNumber)
    {
        int meleeCount = Mathf.CeilToInt(waveNumber * meleeEnemyIncrease);
        int rangeCount = Mathf.CeilToInt(waveNumber * rangeEnemyIncrease);

        float spawnInterval = Mathf.Min(2f, 2.5f - (waveNumber * 0.1f));
        print("Melee: " + meleeCount + " Range: " + rangeCount + " Interval: " + spawnInterval);
        return CreateWave(
            spawnInterval,
            new EnemySpawnData(EnemyType.Melee, meleeCount),
            new EnemySpawnData(EnemyType.Range, rangeCount)
        );
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();

        foreach (EnemySpawnData spawnData in wave.enemiesToSpawn)
        {
            for (int i = 0; i < spawnData.count; i++)
            {
                enemiesToSpawn.Add(spawnData);
            }
        }
        Shuffle(enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            SpawnEnemy(enemiesToSpawn[i].enemyType);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Melee:
                MeleeEnemy meleeEnemy = EnemyPool.Instance.enemyPool.Get();
                meleeEnemy.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                break;
            case EnemyType.Range:
                RangeEnemy rangeEnemy = RangeEnemyPool.Instance.enemyPool.Get();
                rangeEnemy.transform.position = new Vector2(Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
                break;
        }
    }

    private Wave CreateWave(float spawnInterval, params EnemySpawnData[] enemiesToSpawn)
    {
        return new Wave
        {
            enemiesToSpawn = enemiesToSpawn,
            spawnInterval = spawnInterval
        };
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}

[System.Serializable]
public class Wave
{
    public EnemySpawnData[] enemiesToSpawn;
    public float spawnInterval;
}

[System.Serializable]
public class EnemySpawnData
{
    public EnemyType enemyType;
    public int count;

    public EnemySpawnData(EnemyType type, int count)
    {
        this.enemyType = type;
        this.count = count;
    }
}

public enum EnemyType
{
    Melee,
    Range
}
