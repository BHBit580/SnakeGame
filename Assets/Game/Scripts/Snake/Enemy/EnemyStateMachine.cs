using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyStateMachine : StateMachine, ISnake 
{
     [Header("References")]
    public CoolDownSystem coolDownSystem;
    public List<Collider> enemiesInRangeList;
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    
    [Header("Values")]
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 50;
    
    
    [Header("AI")]
    public float foodDetectionRadius = 4.5f;
    public float enemyDetectionRadius = 1.5f;
    public float randomPositionRadius = 5f;
    public float randomPositionTimer = 1f;
    
    
    public int UniqueID { get;private set;}
    public bool IsRotating { get; set; }
    
    [HideInInspector] public Transform headTransform;
    
    private Vector3 targetPosition;
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    private Tween currentRotateTween;
    public Collider[] foodInRangeCollider = new Collider[10];



    private void Start()
    {
        ;
        UniqueID = Random.Range(0, 100000);
        
        headTransform = bodyPartsList[0].transform;
        SnakeHead snakeHead = GetComponentInChildren<SnakeHead>();

        for (int i = 0; i < 25; i++)
        {
            snakeHead.AddBodyPart();
        }

        SwitchState(new EnemyTargetFoodState(this));
    }

    protected override void Update()
    {
        base.Update();
        headTransform.Translate(headTransform.forward * (speed * Time.deltaTime));
        MoveBodyParts(); 
    }
    
    

    
    public void RotateHead(Vector3 directionVector)
    {
        if(IsRotating) return;
        
        if (currentRotateTween != null && currentRotateTween.IsActive())
        {
            currentRotateTween.Kill();
        }
        
        float angle = Vector3.Angle(headTransform.forward, directionVector);                     
        
        if (Vector3.Cross( directionVector , headTransform.forward).y > 0) angle = -angle;                  
        
        Vector3 rotationAngle = new Vector3(0, headTransform.rotation.eulerAngles.y + angle, 0);
        
        currentRotateTween = headTransform.DORotate(rotationAngle , 1/rotationSpeed);
        currentRotateTween.onComplete += () => IsRotating = false;
    }
    
    
    private void DodgeEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(headTransform.position, enemyDetectionRadius, LayerMask.GetMask("BodyPart"));
        enemiesInRangeList = enemiesInRange.Where(collider => collider.transform.root != transform.root).ToList();
        Vector3 awayFromPlayer = transform.position - enemiesInRange[0].transform.position;
        awayFromPlayer = transform.position + awayFromPlayer.normalized * 5f;
        targetPosition = awayFromPlayer;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bodyPartsList[0].transform.position, enemyDetectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bodyPartsList[0].transform.position, foodDetectionRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(bodyPartsList[0].transform.position, randomPositionRadius);
    }
}


