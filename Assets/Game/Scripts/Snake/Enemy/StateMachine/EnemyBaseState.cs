using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool DetectEnemies()
    {
        Ray ray = new Ray(stateMachine.headTransform.position, stateMachine.headTransform.forward);
        
        if (Physics.SphereCast(ray, stateMachine.enemyDetectionRadius,
                out RaycastHit hitInfo,  stateMachine.range, LayerMask.GetMask("BodyPart" , "Wall"), QueryTriggerInteraction.Collide))
        {
            if (hitInfo.collider == null) return false;
            if (hitInfo.transform.parent ==null || hitInfo.transform.parent.gameObject == stateMachine.transform.gameObject) return false;
            
            return true;
        }
        return false;
    }
    
    protected void RefreshArray(Collider[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;
        }
    }

}