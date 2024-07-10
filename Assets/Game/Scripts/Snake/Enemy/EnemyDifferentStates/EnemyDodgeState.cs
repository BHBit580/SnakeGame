using System.Linq;
using UnityEngine;

public class EnemyDodgeState : EnemyBaseState
{
    
    private Collider target;
    public EnemyDodgeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.textUI.text = "Dodge";
        DodgeEnemy();
    }

    public override void Tick(float deltaTime)
    {
        if(DetectChEnemies(out Collider col) == false) stateMachine.SwitchState(new EnemyRandomPosition(stateMachine));
        
        DodgeEnemy();
    }

    public override void Exit()
    {
        
    }
    
    private void DodgeEnemy()
    {
        if (DetectChEnemies(out Collider enemyCollider))
        {
            Vector3 avoidDirection = Vector3.zero;
            avoidDirection = Vector3.Reflect((enemyCollider.transform.position - stateMachine.transform.position),
                enemyCollider.transform.right);
            
            
            stateMachine.RotateHead(avoidDirection);
        }
    }
    
    
    
    
}

/*if(stateMachine.enemiesInRangeList.Count ==0) return;
        
//target = stateMachine.enemiesInRangeList.FirstOrDefault(collider => collider.gameObject.name == "SnakeHead") ?? stateMachine.enemiesInRangeList[0];
//Vector3 direction = (stateMachine.headTransform.position - target.transform.position);

Vector3 averageDirection = Vector3.zero;
        
foreach (var enemy in stateMachine.enemiesInRangeList)
{
    averageDirection += (stateMachine.headTransform.position - enemy.transform.position);
}
        
averageDirection /= stateMachine.enemiesInRangeList.Count;*/
