using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private SnakeGrowthManager growthManager;
    private ISnake iMySnake;

    private void Awake()
    {
        iMySnake = GetComponentInParent<ISnake>();
    }

    private void Start()
    {
        transform.gameObject.layer = LayerMask.NameToLayer("BodyPart");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))           //make only player camera shake
        {
            Destroy(other.gameObject);
            growthManager.AddBodyPart(iMySnake);
        }

        if (other.gameObject.CompareTag("BodyPart"))
        {
            ISnake interfaceOfOther = other.GetComponentInParent<ISnake>();
            if(interfaceOfOther == iMySnake) return;                                   // return as it's our own body part

            //FoodSpawner.Instance.SpawnFoodOnDeath(iMySnake.GetSnakeBeadsList());
            Destroy(transform.parent.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
           // FoodSpawner.Instance.SpawnFoodOnDeath(iMySnake.GetSnakeBeadsList());
            Destroy(transform.parent.gameObject);
        }
    }
}
