using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    Collider[] enemiesArray = new Collider[10];

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool DetectChEnemies(out Collider enemyCollider)
    {
        Ray ray = new Ray(stateMachine.headTransform.position, stateMachine.headTransform.forward);
        
        if (Physics.SphereCast(ray, stateMachine.enemyDetectionRadius,
                out RaycastHit hitInfo,  stateMachine.range, LayerMask.GetMask("BodyPart" , "Wall"), QueryTriggerInteraction.Collide))
        {
            if (hitInfo.collider == null || hitInfo.transform.parent.gameObject == stateMachine.transform.gameObject)
            {
                enemyCollider = null;
                return false;
            }
            
            enemyCollider = hitInfo.collider;
            return true;
        }
        
        enemyCollider = null;
        return false;
    }
    
    protected bool DetectEnemies()
    {
        stateMachine.enemiesInRangeList.Clear();                                        
        RefreshArray(enemiesArray);
        
        Physics.OverlapSphereNonAlloc(stateMachine.headTransform.position, stateMachine.enemyDetectionRadius, enemiesArray , 
            LayerMask.GetMask("BodyPart") , QueryTriggerInteraction.Collide);
        stateMachine.enemiesInRangeList = ExcludeOwnElements();
        
        return stateMachine.enemiesInRangeList.Count > 0;
    }
    
    private List<Collider> ExcludeOwnElements()
    {
        var originalList = enemiesArray.Where(item => item != null && item.transform != null).ToList();
        
        var newList = new List<Collider>();

        foreach (var item in originalList)
        {
            Transform parentTransform = item.transform.parent;
            if(parentTransform.gameObject != stateMachine.transform.gameObject)
            {
                newList.Add(item);
            }
        }

        return newList;
    }

    
    protected void RefreshArray(Collider[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;
        }
    }

}