using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerInScene;
    [SerializeField] private GameObject enemyPrefab;

    
    [SerializeField] private int noToSpawn;
    [SerializeField] private List<GameObject> currentEnemies;
    
    private void Start()
    {
        SpawnEnemies(noToSpawn);
    }

    private void Update()
    {
        CheckEnemiesDestroyedInList();
    }

    private void SpawnEnemies(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateRandomPosition(), Quaternion.identity);
            enemy.transform.SetParent(transform);
            currentEnemies.Add(enemy);
        }
    }
    
    private Vector3 GenerateRandomPosition()
    {
        Vector2 position = Utils.Instance.GetRandomSpawnPositionInsideSpawnArea();
        return new Vector3(position.x, 0f, position.y);
    }
    
    private void CheckEnemiesDestroyedInList()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            if (currentEnemies[i] == null)
            {
                currentEnemies.RemoveAt(i);
                SpawnEnemies(1);
            }
        }
    }
}
