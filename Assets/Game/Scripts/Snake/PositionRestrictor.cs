using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRestrictor : MonoBehaviour
{
    [Header("Restriction")]
    [SerializeField] private Transform minWallX;
    [SerializeField] private Transform maxWallX;
    [SerializeField] private Transform minWallZ;
    [SerializeField] private Transform maxWallZ;

    private void Update()
    {
        RestrictPosition(transform);
    }


    private void RestrictPosition(Transform transform)
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minWallX.position.x, maxWallX.position.x);
        position.z = Mathf.Clamp(position.z, minWallZ.position.z, maxWallZ.position.z);
        transform.position = position;
    }
}
