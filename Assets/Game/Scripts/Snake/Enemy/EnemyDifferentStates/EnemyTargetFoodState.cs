using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTargetFoodState : EnemyBaseState
{

    private Collider targetCollider;
    
    public EnemyTargetFoodState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.textUI.text = "Target Food";
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
        RefreshArray(stateMachine.foodInRangeCollider);
        
        Physics.OverlapSphereNonAlloc(stateMachine.headTransform.position, stateMachine.foodDetectionRadius, stateMachine.foodInRangeCollider,
            LayerMask.GetMask("Food"));

        
        if (targetCollider == null || !stateMachine.foodInRangeCollider.ToList().Contains(targetCollider))
        {
            if (stateMachine.foodInRangeCollider[0] != null)
            {
                targetCollider = stateMachine.foodInRangeCollider[0];
            }
        }
        

        if (targetCollider != null)
        {
            Vector3 direction = targetCollider.transform.position -stateMachine.headTransform.position;
            stateMachine.RotateHead(direction);
        }
    }


    
    private void SwitchToRandomMovingState()
    {
        List<Collider> colliderList = stateMachine.foodInRangeCollider.ToList();

        bool allNull = colliderList.All(collider => collider == null);

        if(allNull)
        {
            stateMachine.SwitchState(new EnemyRandomPosition(stateMachine));
        }
    }
}
