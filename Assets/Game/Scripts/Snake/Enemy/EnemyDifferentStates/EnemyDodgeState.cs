using System.Linq;
using UnityEngine;

public class EnemyDodgeState : EnemyBaseState
{
    public EnemyDodgeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.textUI.text = "Dodge";
    }

    public override void Tick(float deltaTime)
    {
        DetectAndAvoidObstacles();
    }

    public override void Exit()
    {
        
    }
    
    private void DetectAndAvoidObstacles()
    {
        float currentAngle = 0f;
        int attempts = 0;

        while (true) 
        {
            if (!CheckForObstacle(currentAngle))
            {
                stateMachine.RotateHead(Quaternion.Euler(0, currentAngle, 0) * stateMachine.headTransform.transform.forward);
                break;
            }

            attempts++;
            if (attempts % 2 == 1)
            {
                // Odd attempts: check left side
                currentAngle += stateMachine.avoidanceAngle * attempts;
            }
            else
            {
                // Even attempts: check right side
                currentAngle = -stateMachine.avoidanceAngle * attempts;
            }
        }
    }
    
    
    private bool CheckForObstacle(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * stateMachine.headTransform.transform.forward;
        Ray ray = new Ray(stateMachine.headTransform.transform.position, direction);

        return Physics.SphereCast(ray, stateMachine.enemyDetectionRadius, stateMachine.range, LayerMask.GetMask("BodyPart"  , "Wall"), QueryTriggerInteraction.Collide); 
    }
}

