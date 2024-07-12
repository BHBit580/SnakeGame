using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public InputReader inputReader;
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;

    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    public int totalNumber = 25;

   
    void Start() 
    {
        for(int i = 0 ; i< totalNumber; i++)
        {
            IncreaseSnake();
        }
    }

   
    void Update() 
    {
        transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
        
        RotateHead();

        
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * (BodySpeed * Time.deltaTime);

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
    }
    
    private void RotateHead()
    {
        Vector3 targetDirection = inputReader.MouseWorldPosition - transform.position;
        float singleStep = SteerSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDirection).eulerAngles.y, 0);
    }

    private void IncreaseSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }
}