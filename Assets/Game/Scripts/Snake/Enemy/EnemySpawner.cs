using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


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
            enemy.gameObject.name = GenerateRandomName();
            Transform enemyHead = enemy.transform.GetChild(0);

            enemyHead.GetComponent<SnakeHead>().foodSpawner = foodSpawner;
//            enemyHead.GetComponent<EnemyHead>().ground = ground;

            enemy.transform.localPosition = Vector3.zero;
            enemyHead.transform.localPosition = GenerateRandomPosition();
            enemyHead.transform.rotation = Quaternion.identity;

            enemy.transform.SetParent(transform);

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

    private string GenerateRandomName()
    {
        string[] names =
        {
            "Zephy", "Quark", "Nebu", "Galax", "Cosmo", "China" , "Zara",
            "Lumin", "Nova", "Pulsar", "Astra", "Orion", "Rani" , "Mica",
            "Ronaldo", "Messi", "Neymar", "Mbappe", "Suarez", "Hazard" , "Kane",
            "Zenith", "Aurora", "Eclipse", "Comet", "Nimbus" , "Hiya"
        };

        return names[Random.Range(0, names.Length)];
    }
}
