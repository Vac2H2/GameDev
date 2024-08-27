using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] easyEnemies; // Easy enemies for early waves
    [SerializeField] private GameObject[] mediumEnemies; // Medium enemies for mid waves
    [SerializeField] private GameObject[] hardEnemies; // Hard enemies for later waves
    [SerializeField] private GameObject bossPrefab;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8; //Base no. of enemies set per wave
    [SerializeField] private float enemiesPerSecond = 0.5f; //Time between each enemy spawn
    [SerializeField] private float timeBetweenWaves = 5f; //Time interval between waves
    [SerializeField] private float difficultyScalingFactor = 0.8f; //Affects enemy spawning time per wave
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private int maxWave = 8; // Change to increase wave
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;
    private bool isGameOver = false;

    public int CurrentWave => currentWave; //to display wave

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {

        if (isGameOver) return;

        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (isGameOver) yield break;

        isSpawning = true;
        if (currentWave == maxWave)
        {
            SpawnBoss();
        }
        else
        {
            enemiesLeftToSpawn = EnemiesPerWave();
            eps = EnemiesPerSecond();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        if (currentWave == maxWave)
        {
            isGameOver = true;
        }
        else
        {
            currentWave++;
            StartCoroutine(StartWave());
        }
    }

    private void SpawnEnemy()
    {
        GameObject[] enemyPool = GetEnemyPoolForCurrentWave();
        int index = Random.Range(0, enemyPool.Length);
        GameObject prefabToSpawn = enemyPool[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private GameObject[] GetEnemyPoolForCurrentWave()
    {
        if (currentWave <= 1)
        {
            return easyEnemies;
        }
        else if (currentWave <= 4)
        {
            return CombineArrays(easyEnemies, mediumEnemies);
        }
        else
        {
            return CombineArrays(easyEnemies, mediumEnemies, hardEnemies);
        }
    }

    private GameObject[] CombineArrays(params GameObject[][] arrays)
    {
        List<GameObject> combinedList = new List<GameObject>();
        foreach (GameObject[] array in arrays)
        {
            combinedList.AddRange(array);
        }
        return combinedList.ToArray();
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, LevelManager.main.startPoint.position, Quaternion.identity);
        enemiesLeftToSpawn = 0;
        eps = 0;
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); //No. of enemies per wave scaled to difficulty    
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap); //No. of enemies per wave scaled to difficulty    
    }

}