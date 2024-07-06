using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOver : MonoBehaviour
{
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Transform endingPosition;
  

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
}
