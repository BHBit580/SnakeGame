using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerSnakeMovement : MonoBehaviour , ISnake{
    
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CoolDownSystem coolDownSystem;
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    
    [Header("Values")]
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 50;
    
    

    
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    private Vector3 directionVector;
    private Transform headTransform;
    private float rotationAngle;

    private void Start()
    {
        headTransform = bodyPartsList[0].transform;
    }

    private void Update()
    {
        RotateHead();
        MoveHead();
        MoveBodyParts();
    }

    private void MoveHead()
    {
        headTransform.Translate(headTransform.forward * (speed * Time.deltaTime), Space.World);
    }

    private void RotateHead()
    {
        Vector3 targetDirection = inputReader.MouseWorldPosition - headTransform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(headTransform.forward, targetDirection, singleStep, 0.0f);
        headTransform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDirection).eulerAngles.y, 0);
    }

    
    private void MoveBodyParts()
    {
        for (int i = 1; i < bodyPartsList.Count; i++)
        {
            curBodyPart = bodyPartsList[i];
            PrevBodyPart = bodyPartsList[i - 1];

            dis = Vector3.Distance(PrevBodyPart.position,curBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            newpos.y = headTransform.position.y;

            float T = Time.deltaTime * dis / minDistance * speed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart(Transform newPart)
    {
        bodyPartsList.Add(newPart);
    }

    public List<Transform> GetSnakeBeadsList()
    {
        return bodyPartsList;
    }
}