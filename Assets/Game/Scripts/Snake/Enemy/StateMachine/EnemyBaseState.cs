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
    
    protected void DetectEnemies()
    {
        stateMachine.enemiesInRangeList.Clear();                                        
        RefreshArray(enemiesArray);
        
        Physics.OverlapSphereNonAlloc(stateMachine.headTransform.position, stateMachine.enemyDetectionRadius, enemiesArray , 
            LayerMask.GetMask("BodyPart") , QueryTriggerInteraction.Collide);
        stateMachine.enemiesInRangeList = ExcludeOwnElements();
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