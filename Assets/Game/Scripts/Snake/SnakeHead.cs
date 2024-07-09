using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public float minScale = 0.7f;
    public float maxScale = 1.5f;
    public float sizeAdditiveFactor = 0.5f;
    public GameObject beadsPrefab;
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
            AddBodyPart();
        }
        
        
        
        if (other.gameObject.CompareTag("BodyPart")) 
        {
            ISnake interfaceOfOther = other.GetComponentInParent<ISnake>();
            if(interfaceOfOther == iMySnake) return;                                   // return as it's our own body part
            
            FoodSpawner.Instance.SpawnFoodOnDeath(iMySnake.GetSnakeBeadsList());
            Destroy(transform.parent.gameObject);
        }
    }
    
    
    public void AddBodyPart()
    {
        List<Transform> bodyParts = iMySnake.GetSnakeBeadsList();
        Transform newPart = (Instantiate (beadsPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.gameObject.tag = "BodyPart";
        newPart.gameObject.layer= LayerMask.NameToLayer("BodyPart");
        newPart.SetParent(transform.parent);
        iMySnake.AddBodyPart(newPart);
        IncreaseBodyPartOfEachBead(bodyParts);
    }

    private void IncreaseBodyPartOfEachBead(List<Transform> bodyParts)
    {
        float newScale = transform.localScale.x + sizeAdditiveFactor/100;
        newScale = Mathf.Clamp(newScale, minScale, maxScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        foreach (Transform bodyPart in bodyParts)
        {
            if(bodyPart == transform) continue;
            bodyPart.localScale = transform.localScale;
        }
    }
    
}
