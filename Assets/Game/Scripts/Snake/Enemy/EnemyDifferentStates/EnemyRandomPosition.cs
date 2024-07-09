using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRandomPosition : EnemyBaseState , IHasCoolDown
{
    public EnemyRandomPosition(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.textUI.text = "Random";
        TargetRandomPositions();
    }

    public override void Tick(float deltaTime)
    {
        DetectEnemies();
        if(stateMachine.enemiesInRangeList.Count>0) stateMachine.SwitchState(new EnemyDodgeState(stateMachine));
        
        SwitchToTargetFoodState();
        TargetRandomPositions();
    }

    public override void Exit()
    {
        
    }
    
    
    
    
    
    private void SwitchToTargetFoodState()
    {
        Physics.OverlapSphereNonAlloc(stateMachine.headTransform.position, stateMachine.foodDetectionRadius, stateMachine.foodInRangeCollider,
            LayerMask.GetMask("Food"));
        
        if (stateMachine.foodInRangeCollider.Length > 0 && stateMachine.foodInRangeCollider[0] !=null)
        {
            stateMachine.SwitchState(new EnemyTargetFoodState(stateMachine));
        }
    }
    
    private void TargetRandomPositions()
    {
        bool isInCoolDown = stateMachine.coolDownSystem.IsInCoolDown(stateMachine.UniqueID);
        if (isInCoolDown) return;
    
        float randomAngle = Random.Range(-45 , 45);
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.up) * stateMachine.headTransform.forward;
        
        stateMachine.RotateHead(direction);
    
        Debug.DrawRay(stateMachine.headTransform.position, direction, Color.yellow);
        stateMachine.coolDownSystem.StartCoolDown(this);
    }

    public int ID => stateMachine.UniqueID;
    public float CoolDownDuration => stateMachine.randomPositionTimer;
}
