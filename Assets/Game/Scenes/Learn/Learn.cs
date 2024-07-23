using UnityEngine;

public class Learn : MonoBehaviour
{
    private void Start()
    {
        Transform ourParentTransform = transform.parent.transform.parent;

        Debug.Log(ourParentTransform.name);
    }
}
