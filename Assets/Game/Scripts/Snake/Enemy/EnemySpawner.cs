using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Utils utils;
    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private int noToSpawn;
    [SerializeField] private List<GameObject> currentEnemies;
    [SerializeField] MeshRenderer ground;


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
            GameObject enemy = Instantiate(enemyPrefab , Vector3.zero , Quaternion.identity);
            Transform enemyHead = enemy.transform.GetChild(0);

            enemyHead.GetComponent<EnemyHead>().foodSpawner = foodSpawner;
            enemyHead.GetComponent<EnemyHead>().ground = ground;

            enemy.transform.SetParent(transform);
            enemy.transform.localPosition = Vector3.zero;

            enemyHead.transform.localPosition = GenerateRandomPosition();
            enemyHead.transform.rotation = Quaternion.identity;

            currentEnemies.Add(enemy);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector2 position = utils.GetRandomSpawnPositionInsideSpawnArea();
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
