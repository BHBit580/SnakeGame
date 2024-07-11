using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyStateMachine : StateMachine, ISnake 
{
    [Header("References")]
    
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    
    [Header("Values")]
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 50;
    
    
    [Header("AI")]
    public float foodDetectionRadius = 4.5f;
    public float enemyDetectionRadius = 1.5f;
    public float randomPositionTimer = 1f;
    public float avoidanceAngle = 15f;
    
    public float range;
    
    
    public int UniqueID { get;private set;}
    
    [HideInInspector] public Transform headTransform;
    
    private Vector3 targetPosition;
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;

    public CoolDownSystem coolDownSystem { get; private set; }
    public TextMeshProUGUI textUI;
    public Collider[] foodInRangeCollider = new Collider[10];


    private void Start()
    {
        coolDownSystem = Utils.Instance.CoolDownSystem;
        UniqueID = Random.Range(0, 100000);
        
        headTransform = bodyPartsList[0].transform;

        SnakeGrowthManager snakeGrowthManager = GetComponentInChildren<SnakeGrowthManager>();

        for (int i = 0; i < 5; i++)
        {
            snakeGrowthManager.AddBodyPart(this);
        }
        
        SwitchState(new EnemyDodgeState(this));
    }

    protected override void Update()
    {
        base.Update();
        headTransform.Translate(headTransform.forward * (speed * Time.deltaTime) , Space.World);
        MoveBodyParts(); 
    }
    
    
    public void RotateHead(Vector3 targetDirection)
    {
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
    
    private void OnDrawGizmos()
    {
        Vector3 origin = bodyPartsList[0].transform.position;
        Vector3 direction = bodyPartsList[0].transform.forward;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * range);
        Gizmos.DrawWireSphere(origin + direction * range, enemyDetectionRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, foodDetectionRadius);
    }
}


