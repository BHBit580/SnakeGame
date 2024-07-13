using System.Linq;
using UnityEngine;

public class EnemyDodgeState : EnemyBaseState
{
    private int maximumAttempts = 50;
    public EnemyDodgeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.textUI.text = "Dodge";
    }

    public override void Tick(float deltaTime)
    {
        if(!DetectEnemies()) stateMachine.SwitchState(new EnemyRandomPosition(stateMachine));
        DetectAndAvoidObstacles();
    }

    public override void Exit()
    {
        
    }
    
    private void DetectAndAvoidObstacles()
    {
        float currentAngle = 0f;
        int attempts = 0;

        for(int i = 0; i < maximumAttempts; i++)
        {
            if (!RaycastAndCheckForObstacleFuture(currentAngle))
            {
                stateMachine.RotateHead(Quaternion.Euler(0, currentAngle, 0) * stateMachine.headTransform.transform.forward);
                break;
            }

            attempts++;
            if (attempts % 2 == 1)
            {
                currentAngle += stateMachine.avoidanceAngle * attempts;
            }
            else
            {
                currentAngle = -stateMachine.avoidanceAngle * attempts;
            }
        }
    }
    
    
    private bool RaycastAndCheckForObstacleFuture(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * stateMachine.headTransform.transform.forward;
        Ray ray = new Ray(stateMachine.headTransform.transform.position, direction);

        return Physics.SphereCast(ray, stateMachine.enemyDetectionRadius, stateMachine.range, LayerMask.GetMask("BodyPart"  , "Wall"), QueryTriggerInteraction.Collide); 
    }
}
 
