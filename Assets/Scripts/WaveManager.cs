using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Ennemy ennemyPrefab;
    public Transform player;
    public float minSpawnDistance;

    private int currentWaveIdx = -1;
    private Wave currentWave;
    private int remainingToSpawn = 0;
    private int remainingAlive = 0;
    private float lastSpawnTime;

    [System.Serializable]
    public class Wave
    {
        public int nbEnnemies;
        public float timeBetweenSpawns;
    }

    private void Start()
    {
        lastSpawnTime = Time.time;
        SpawnNextWave();
    }

    private Vector3 generateAvailableSpawnPosition()
    {
        Vector3 pos;
        do
        {
            pos = new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45));
        } while (Vector3.Distance(pos, player.position) < minSpawnDistance);
        return pos;
    }

    private void Update()
    {
        if (!player)
            return;
        if (remainingToSpawn <= 0)
            return;
        if (Time.time - lastSpawnTime < currentWave.timeBetweenSpawns)
            return;
        lastSpawnTime = Time.time;
        Ennemy spawnedEntity = Instantiate(ennemyPrefab, generateAvailableSpawnPosition(), Quaternion.identity);
        spawnedEntity.OnDeath += OnEnnemyDeath;
        remainingToSpawn -= 1;
    }

    private void SpawnNextWave()
    {
        currentWaveIdx += 1;
        if (currentWaveIdx >= waves.Length)
        {
            StartCoroutine(LoadLevelAfterDelay(3f));
            return;
        }
        currentWave = waves[currentWaveIdx];
        remainingToSpawn = currentWave.nbEnnemies;
        remainingAlive = remainingToSpawn;
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Single);
    }

    void OnEnnemyDeath()
    {
        remainingAlive -= 1;
        if (remainingAlive <= 0)
            SpawnNextWave();
    }
}
