using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeMovement : MonoBehaviour , ISnake
{
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    
    [Header("Values")]
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 50;
    
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;


    private void Start()
    {
        SnakeHead snakeHead = GetComponentInChildren<SnakeHead>();

        for (int i = 0; i < 25; i++)
        {
            snakeHead.AddBodyPart();
        }
    }

    private void Update()
    {
        MoveHead();
        MoveBodyParts();
    }

    private void MoveHead()
    {
        bodyPartsList[0].Translate(bodyPartsList[0].forward * (speed * Time.deltaTime), Space.World);

       /* if (inputReader.HorizontalInput != 0)
        {
            bodyPartsList[0].Rotate(Vector3.up * (rotationSpeed * Time.deltaTime * inputReader.HorizontalInput));
        }*/
    }

    
    private void MoveBodyParts()
    {
        for (int i = 1; i < bodyPartsList.Count; i++)
        {
            curBodyPart = bodyPartsList[i];
            PrevBodyPart = bodyPartsList[i - 1];

            dis = Vector3.Distance(PrevBodyPart.position,curBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            newpos.y = bodyPartsList[0].position.y;

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
