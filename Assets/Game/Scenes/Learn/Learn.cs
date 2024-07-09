using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Learn : MonoBehaviour
{
    public InputReader inputReader;
    public float moveSpeed = 1f;
    public float speed = 1.0f;

    void Update()
    {
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);
        Vector3 targetDirection = inputReader.MouseWorldPosition - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDirection).eulerAngles.y, 0);
    }
}
