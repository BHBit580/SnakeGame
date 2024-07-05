using System;
using DG.Tweening;
using UnityEngine;

public class Learn : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    public bool isRotating = false;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (isRotating) return; 
        Debug.Log("Rotating"); 
        Tween rotation =  transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 100, 0), 1 / speed); 
        isRotating = true;
        rotation.onComplete += () => isRotating = false;
    }
}
