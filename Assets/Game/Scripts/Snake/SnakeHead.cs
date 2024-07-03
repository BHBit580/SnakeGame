using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public GameObject beadsPrefab;
    private ISnake iSnake;

    private void Awake()
    {
        iSnake = GetComponentInParent<ISnake>();
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
            if(interfaceOfOther == iSnake) return;                                   // return as it's our own body part
            
            Destroy(transform.parent.gameObject);
        }
    }
    
    
    public void AddBodyPart()
    {
        List<Transform> bodyParts = iSnake.GetSnakeBeadsList();
        Transform newPart = (Instantiate (beadsPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.gameObject.tag = "BodyPart";
        newPart.gameObject.layer= LayerMask.NameToLayer("BodyPart");
        newPart.SetParent(transform.parent);
        iSnake.AddBodyPart(newPart);
    }
}
