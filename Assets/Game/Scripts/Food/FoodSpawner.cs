using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : GenericSingleton<FoodSpawner>
{
    [SerializeField] private int noToSpawn;
    [SerializeField] private GameObject[] foodPrefabs;
    
    private List<GameObject> foodList = new List<GameObject>();


    private void Start()
    {
        SpawnFood(noToSpawn);
    }
    
    private Vector3 GenerateRandomPosition()
    {
        Vector2 position = Utils.Instance.GetRandomSpawnPositionInsideSpawnArea();
        return new Vector3(position.x, 1f, position.y);
    }

    private void Update()
    {
        CheckFoodDestroyedInList();
    }

    private void CheckFoodDestroyedInList()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (foodList[i] == null)
            {
                foodList.RemoveAt(i);
                SpawnFood(1);
            }
        }
    }

    private void SpawnFood(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject food = Instantiate(foodPrefabs[Random.Range(0 , foodPrefabs.Length-1)], GenerateRandomPosition(), Quaternion.identity);
            food.layer = LayerMask.NameToLayer("Food");
            food.tag = "Food";
            food.transform.SetParent(transform);
            foodList.Add(food);
        }
    }

    public void SpawnFoodOnDeath(List<Transform> beadsTransform)
    {
        foreach (Transform bead in beadsTransform)
        {
            GameObject food = Instantiate(foodPrefabs[Random.Range(0 , foodPrefabs.Length-1)], bead.position, bead.rotation);
            food.layer = LayerMask.NameToLayer("Food");
            food.tag = "Food";
            food.transform.SetParent(transform);
            foodList.Add(food);
        }
    }
}
