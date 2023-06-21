using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public int initialEnemies = 10;
    public float spawnInterval = 10f;
    private void Start()
    {
        SpawnEnemies();
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < initialEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
