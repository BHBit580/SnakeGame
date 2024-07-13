using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.GetChild(0).transform.position = GenerateRandomPosition();
            Quaternion randomAngle = Quaternion.Euler(Quaternion.identity.eulerAngles.x , Random.Range(0 , 360) , Quaternion.identity.eulerAngles.z);
            enemy.transform.GetChild(0).transform.rotation = randomAngle;
            enemy.transform.SetParent(transform);
            currentEnemies.Add(enemy);
        }
    }
    
    private Vector3 GenerateRandomPosition()
    {
        Vector2 position = Utils.Instance.GetRandomSpawnPositionInsideSpawnArea();
        return new Vector3(position.x, 1f, position.y);
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
