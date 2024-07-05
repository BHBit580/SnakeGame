using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomPosition : EnemyBaseState , IHasCoolDown
{
    public EnemyRandomPosition(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        TargetRandomPositions();
    }

    public override void Tick(float deltaTime)
    {
        TargetRandomPositions();
    }

    public override void Exit()
    {
        
    }
    
    private void TargetRandomPositions()
    {
        bool isInCoolDown = stateMachine.coolDownSystem.IsInCoolDown(stateMachine.UniqueID);
        if (isInCoolDown) return;
    
        float randomAngle = Random.Range(-45 , 45);
        Debug.Log(randomAngle);
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.up) * stateMachine.headTransform.forward;
        
        stateMachine.RotateHead(direction);
    
        Debug.DrawRay(stateMachine.headTransform.position, direction, Color.yellow);
        stateMachine.coolDownSystem.StartCoolDown(this);
    }

    public int ID => stateMachine.UniqueID;
    public float CoolDownDuration => stateMachine.randomPositionTimer;
}
