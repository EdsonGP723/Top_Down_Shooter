using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public int baseEnemiesPerWave = 3;
    public float enemiesIncreasePerWave = 1.5f;
    public float timeBetweenWaves = 5f;
    public float spawnDelay = 0.5f;
    public int enemiesAlive = 0;

    private bool isWaveActive = false;
    private EnemyPool enemyPool;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        enemyPool = EnemyPool.instance;
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (!isWaveActive)
        {
            currentWave++;
            int enemiesInWave = Mathf.RoundToInt(baseEnemiesPerWave * Mathf.Pow(enemiesIncreasePerWave, currentWave - 1));
            StartCoroutine(SpawnWave(enemiesInWave));
        }
    }

    private IEnumerator SpawnWave(int enemyCount)
    {
        isWaveActive = true;
        Debug.Log($"Starting Wave {currentWave} with {enemyCount} enemies");

        // Asegurarse de que el pool tenga suficientes enemigos
        while (enemyPool.pooledEnemies.Count < enemyCount)
        {
            GameObject newEnemy = Instantiate(enemyPool.enemyPrefab);
            newEnemy.SetActive(false);
            enemyPool.pooledEnemies.Add(newEnemy);
        }

        // Spawning enemies
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = enemyPool.GetPooledObject();
            if (enemy != null)
            {
                enemiesAlive++;
                enemy.transform.position = GetRandomSpawnPosition();
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void OnEnemyKilled()
    {
        if (enemiesAlive <= 0)
        {
            isWaveActive = false;
            StartCoroutine(StartNextWaveWithDelay());
        }
    }

    private IEnumerator StartNextWaveWithDelay()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartNextWave();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Obtener un punto aleatorio fuera de la pantalla
        Camera cam = Camera.main;
        float padding = 2f;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        Vector3 spawnPos = new Vector3(
            randomX < 0 ? -cam.orthographicSize * cam.aspect - padding : cam.orthographicSize * cam.aspect + padding,
            randomY < 0 ? -cam.orthographicSize - padding : cam.orthographicSize + padding,
            0
        );

        return spawnPos;
    }
}
