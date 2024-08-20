using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _minimumSpawnTime = 1f;
    [SerializeField]
    private float _maximumSpawnTime = 5f;
    private float _timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            SpawnEnemy();
            SetTimeUntilSpawn();
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyPrefab != null)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Enemy spawned at position: {transform.position}");
        }
        else
        {
            Debug.LogError("Enemy prefab is not assigned!");
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
