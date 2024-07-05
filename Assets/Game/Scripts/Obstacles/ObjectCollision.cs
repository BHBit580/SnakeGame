using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private Vector3 enterPosition;
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Ground")) return;
        enterPosition = other.transform.position;
        RestrictPosition(other);
    }
    
    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Ground")) return;
        
        RestrictPosition(other);
    }
    
    private void RestrictPosition(Collision other)
    {
        other.transform.position = enterPosition;
    }
}