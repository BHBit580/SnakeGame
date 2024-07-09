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
        if(stateMachine.enemiesInRangeList.Count() ==0) stateMachine.SwitchState(new EnemyRandomPosition(stateMachine));
        
        DodgeEnemy();
    }

    public override void Exit()
    {
        
    }
    
    private void DodgeEnemy()
    {
        DetectEnemies();
        
        if(stateMachine.enemiesInRangeList.Count ==0) return;
        
        target = stateMachine.enemiesInRangeList.FirstOrDefault(collider => collider.gameObject.name == "SnakeHead") ?? stateMachine.enemiesInRangeList[0];
        
        Vector3 direction = (stateMachine.headTransform.position - target.transform.position);
        stateMachine.RotateHead(direction);
    }
    
    
    
    
}
