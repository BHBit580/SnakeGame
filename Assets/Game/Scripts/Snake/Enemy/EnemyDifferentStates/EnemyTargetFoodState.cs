using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetFoodState : EnemyBaseState
{
    public EnemyTargetFoodState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        TargetFood();
    }

    public override void Exit()
    {
        
    }
    
    private void TargetFood()
    {
        if(stateMachine.IsRotating) return;
        
        Collider[] foodInRange = Physics.OverlapSphere(stateMachine.headTransform.position, stateMachine.foodDetectionRadius,
            LayerMask.GetMask("Food"));
        if (foodInRange.Length > 0)
        {
             Vector3 direction = stateMachine.headTransform.position - foodInRange[0].transform.position;
             stateMachine.RotateHead(direction);
        }
        
        if(foodInRange.Length ==0) stateMachine.SwitchState(new EnemyRandomPosition(stateMachine));
    }
}
