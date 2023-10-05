using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    public EnumManager.EnemyCode type;
    public float spawnRate;
}

[System.Serializable]
public class Wave
{
    public List<WaveEnemy> enemyTypes;
    public int minEnemyCount;
    public int maxEnemyCount;
    public float spawnInterval;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    public int time;

    public bool bossWave;
    public EnumManager.EnemyCode bossCode;
}

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    private Transform player;

    List<Vector2> spawnPosList = new List<Vector2>();
    List<GameObject> enemyList = new List<GameObject>();

    WaitForSeconds waitUntilNewWave = new WaitForSeconds(2f);
    WaitForSeconds waitSpawnWarningTime = new WaitForSeconds(1f);
    WaitForSeconds waitSpawnInterval = new WaitForSeconds(0.04f);

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        player = PlayerCtrl.Instance.transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
        GameManager.Instance.OnWaveStop += OnGameStop;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
        GameManager.Instance.OnWaveStop -= OnGameStop;
    }

    public void SpawnWave(Wave currentWave)
    {
        StartCoroutine(SpawnWaveCoroutine(currentWave));
    }

    public IEnumerator SpawnWaveCoroutine(Wave currentWave)
    {
        if (currentWave.bossWave)
        {
            Vector3 spawnPosition = FindRandomSpawnPosition(currentWave);
            GameObject bossGO = GetPrefabByName(currentWave.bossCode.ToString());
            Spawn(bossGO, spawnPosition, Quaternion.identity);
        }

        yield return waitUntilNewWave;

        for (int i = 0; i < 999; i++)
        {
            spawnPosList.Clear();
            enemyList.Clear();

            int totalEnemyCount = Random.Range(currentWave.minEnemyCount, currentWave.maxEnemyCount + 1);

            for (int j = 0; j < totalEnemyCount; j++)
            {
                GameObject selectedEnemyPrefab = SelectRandomEnemyPrefab(currentWave.enemyTypes);
                Vector3 spawnPosition = FindRandomSpawnPosition(currentWave);
                spawnPosList.Add(spawnPosition);
                enemyList.Add(selectedEnemyPrefab);
            }

            foreach (Vector2 spawnPos in spawnPosList)
            {
                yield return waitSpawnInterval;
                FXSpawner.Instance.Spawn(EnumManager.FXCode.CROSS_WARNING.ToString(), spawnPos, Quaternion.identity);
            }

            yield return waitSpawnWarningTime;

            for (int j = 0; j < enemyList.Count; j++)
            {
                Spawn(enemyList[j], spawnPosList[j], Quaternion.identity);
            }

            if (GameManager.Instance.leftTime <= 1f) break;
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }
    }

    GameObject SelectRandomEnemyPrefab(List<WaveEnemy> enemyTypes)
    {
        float randomSpawnRate = Random.Range(0f, 1f);
        float cumulativeRate = 0f;
        GameObject selectedEnemyPrefab = null;

        foreach (var enemyType in enemyTypes)
        {
            cumulativeRate += enemyType.spawnRate;
            if (randomSpawnRate <= cumulativeRate)
            {
                selectedEnemyPrefab = GetPrefabByName(enemyType.type.ToString());
                break;
            }
        }

        return selectedEnemyPrefab;
    }

    Vector3 FindRandomSpawnPosition(Wave currentWave)
    {
        Vector3 playerPosition = player.position;
        Vector2 minSpawnPos = new(Mathf.Max(MapManager.Instance.mapBounds.bounds.min.x, playerPosition.x - currentWave.maxDistanceToPlayer), Mathf.Max(MapManager.Instance.mapBounds.bounds.min.y, playerPosition.y - currentWave.maxDistanceToPlayer));
        Vector2 maxSpawnPos = new(Mathf.Min(MapManager.Instance.mapBounds.bounds.max.x, playerPosition.x + currentWave.maxDistanceToPlayer), Mathf.Min(MapManager.Instance.mapBounds.bounds.max.y, playerPosition.y + currentWave.maxDistanceToPlayer));

        Vector3 spawnPosition;
        int max = 100;
        do
        {
            float randomX = Random.Range(minSpawnPos.x, maxSpawnPos.x);
            float randomY = Random.Range(minSpawnPos.y, maxSpawnPos.y);
            spawnPosition = new Vector3(randomX, randomY, 0f);
            max++;
        } while (IsSpawnPositionTooCloseToPlayer(spawnPosition, currentWave.minDistanceToPlayer) && max <= 100);

        return spawnPosition;
    }

    bool IsSpawnPositionTooCloseToPlayer(Vector3 spawnPosition, float minDistance)
    {
        return spawnPosition.x > player.position.x - minDistance && spawnPosition.x < player.position.x + minDistance && spawnPosition.y > player.position.y - minDistance && spawnPosition.y < player.position.y + minDistance;
    }

    void OnGameStop()
    {
        StopAllCoroutines();
    }

    void OnGameStart()
    {
        DespawnAllActiveObject();
    }
}
