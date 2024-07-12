using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class PlayerController : MonoBehaviour , ISnake , IHasCoolDown{
    
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private VoidEventChannelSO playerDecreaseScore;
    [SerializeField] private IntVariableSO playerScore;
    [SerializeField] private CoolDownSystem coolDownSystem;
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    
    [Header("Values")]
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float normalSpeed = 1;
    [SerializeField] private float fastSpeed = 2;
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private float scoreDecreaseCoolDownTime = 1.5f;
    

    
    private float dis , rotationAngle , currentSpeed;
    private const int UniqueID = 123456789;
    private Transform curBodyPart, PrevBodyPart, headTransform;
    private Vector3 directionVector;
    private SnakeGrowthManager snakeGrowthManager;

    private void Start()
    {
        headTransform = bodyPartsList[0].transform;
        snakeGrowthManager = GetComponentInChildren<SnakeGrowthManager>();
        coolDownSystem.StartCoolDown(this);
    }

    private void Update()
    {
        MoveHead();
        MoveBodyParts();
        RotateHead();
        DecreaseLengthOverTime();
    }

    private void MoveHead()
    {
        currentSpeed = inputReader.IsFastSpeed ? fastSpeed : normalSpeed;

        if (playerScore.data == 0) currentSpeed = normalSpeed;             //if the score is 0 then slow it ! 
        headTransform.Translate(headTransform.forward * (currentSpeed * Time.deltaTime), Space.World);
    }

    private void RotateHead()
    {
        Vector3 targetDirection = inputReader.MouseWorldPosition - headTransform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(headTransform.forward, targetDirection, singleStep, 0.0f);
        headTransform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDirection).eulerAngles.y, 0);
    }

    private void DecreaseLengthOverTime()
    {
        if (!coolDownSystem.IsInCoolDown(UniqueID) && inputReader.IsFastSpeed && playerScore.data > 0)
        {
            snakeGrowthManager.DecreaseBodyPart(bodyPartsList);
            playerDecreaseScore?.RaiseEvent();
            coolDownSystem.StartCoolDown(this);
        }
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

            float T = Time.deltaTime * dis / minDistance * currentSpeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }

    #region InterfaceMethods
    
    public void AddBodyPart(Transform newPart)
    {
        bodyPartsList.Add(newPart);
    }

    public List<Transform> GetSnakeBeadsList()
    {
        return bodyPartsList;
    }

    public int ID => UniqueID;
    
    public float ScoreDecreaseCoolDownDuration => scoreDecreaseCoolDownTime;
    
    #endregion
}