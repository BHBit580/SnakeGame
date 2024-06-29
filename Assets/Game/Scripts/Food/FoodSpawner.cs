using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Food Spawner Area Range")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    [SerializeField] private int noToSpawn;
    [SerializeField] private GameObject foodPrefab;
    
    public List<GameObject> foodList = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < noToSpawn; i++)
        {
            GameObject food = Instantiate(foodPrefab, GenerateRandomPosition(), Quaternion.identity);
            food.transform.SetParent(transform);
            foodList.Add(food);
        }
    }
    
    private Vector3 GenerateRandomPosition()
    {
        float x = UnityEngine.Random.Range(minX, maxX);
        float z = UnityEngine.Random.Range(minZ, maxZ);
        return new Vector3(x, 1.5f, z);
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
            }
        }
    }
    
    
}
