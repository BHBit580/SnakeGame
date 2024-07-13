using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGrowthManager : MonoBehaviour
{
    [SerializeField] private GameObject beadsPrefab;
    public float minScale = 0.7f;
    public float maxScale = 1.5f;
    public float sizeAdditiveFactor = 0.5f;
    
    public void AddBodyPart(ISnake iMySnake)
    {
        List<Transform> bodyParts = iMySnake.GetSnakeBeadsList();
        Transform newPart = (Instantiate (beadsPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.gameObject.tag = "BodyPart";
        newPart.gameObject.layer= LayerMask.NameToLayer("BodyPart");
        newPart.SetParent(transform.parent);
        iMySnake.AddBodyPart(newPart);
        IncreaseBodyPartOfEachBead(bodyParts);
    }

    private void IncreaseBodyPartOfEachBead(List<Transform> bodyParts)
    {
        float newScale = transform.localScale.x + sizeAdditiveFactor/100;
        newScale = Mathf.Clamp(newScale, minScale, maxScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        foreach (Transform bodyPart in bodyParts)
        {
            if(bodyPart == transform) continue;
            bodyPart.localScale = transform.localScale;
        }
    }
    
    private void DecreaseBodyPartOfEachBead(List<Transform> bodyParts)
    {
        float newScale = transform.localScale.x - sizeAdditiveFactor/100;
        newScale = Mathf.Clamp(newScale, minScale, maxScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        foreach (Transform bodyPart in bodyParts)
        {
            if(bodyPart == transform) continue;
            bodyPart.localScale = transform.localScale;
        }
    }

    public void DecreaseBodyPart(List<Transform> bodyPartsList)
    { 
        DecreaseBodyPartOfEachBead(bodyPartsList);
       int lastIndex = bodyPartsList.Count - 1;
       if(lastIndex <=4) return;
       GameObject lastBead = bodyPartsList[lastIndex].gameObject;
       bodyPartsList.RemoveAt(lastIndex);
       Destroy(lastBead);
    }
}
