using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Learn : MonoBehaviour
{
    public float range = 2f;
    public float enemyDetectionRadius = 1.5f;
    
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Gizmos.color = Color.white;
        Gizmos.DrawLine(origin, origin + direction * range);
        Gizmos.DrawWireSphere(origin + direction * range, enemyDetectionRadius);
    }

    private void Update()
    {
        DetectChEnemies();
    }

    protected bool DetectChEnemies()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.SphereCast(ray, enemyDetectionRadius,
                out RaycastHit hitInfo,  range, LayerMask.GetMask("BodyPart"), QueryTriggerInteraction.Collide))
        {
            if(hitInfo.collider != null)
            {
                Debug.Log("AAAA");
                DebugReflectVector(hitInfo.collider);
                return true;
            }
        }
        return false;
    }

    private void DebugReflectVector(Collider hitInfo)
    {
        Vector3 inNormal = hitInfo.transform.right;
        Vector3 direction = hitInfo.transform.position - transform.position;
        Debug.DrawRay(transform.position , direction , Color.blue);
        Vector3 outDirection = Vector3.Reflect(direction, inNormal);
        Debug.DrawRay(transform.position, outDirection, Color.yellow);
    }

}
