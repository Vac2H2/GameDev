using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _minimumSpawnTime = 1f;
    [SerializeField]
    private float _maximumSpawnTime = 5f;
    private float _timeUntilSpawn;
    private Transform playerTransform;

    void Awake()
    {
        SetTimeUntilSpawn();
    }
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
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
            GameObject instantiatedEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            FollowScript followScript = instantiatedEnemy.GetComponent<FollowScript>();
            if (followScript != null)
            {
                followScript.targetObj = playerTransform;
            }
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
