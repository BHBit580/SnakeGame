using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : GenericSingleton<FoodSpawner>
{
    [SerializeField] private int noToSpawn;
    [SerializeField] private GameObject foodPrefab;
    
    public List<GameObject> foodList = new List<GameObject>();


    private void Start()
    {
        SpawnFood(noToSpawn);
    }
    
    private Vector3 GenerateRandomPosition()
    {
        Vector2 position = Utils.Instance.GetRandomSpawnPositionInsideSpawnArea();
        return new Vector3(position.x, 1.5f, position.y);
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
            Quaternion randomAngle = Quaternion.Euler(Random.Range(0, 360), 
                Random.Range(0, 360), Random.Range(0, 360));
            GameObject food = Instantiate(foodPrefab, GenerateRandomPosition(), randomAngle);
            food.layer = LayerMask.NameToLayer("Food");
            food.transform.SetParent(transform);
            foodList.Add(food);
        }
    }

    public void SpawnFoodOnDeath(List<Transform> beadsTransform)
    {
        foreach (Transform bead in beadsTransform)
        {
            GameObject food = Instantiate(foodPrefab, bead.position, bead.rotation);
            food.layer = LayerMask.NameToLayer("Food");
            food.transform.SetParent(transform);
            foodList.Add(food);
        }
    }
}
