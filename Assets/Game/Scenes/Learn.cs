using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Learn : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        Vector3 forward = transform.forward;

        float angle = Vector3.SignedAngle(targetDirection, forward, Vector3.up);
        Debug.Log(angle);
        
        Debug.DrawRay(transform.position , targetDirection , Color.red);
    }
}
