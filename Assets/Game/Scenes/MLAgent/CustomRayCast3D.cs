using System.Collections.Generic;
using Unity.MLAgents.Policies;
using UnityEngine;


public class CustomRaycast3D : MonoBehaviour
{
    [SerializeField] private List<string> detectableTags;
    [SerializeField] private float angle = 90;
    [SerializeField] private int numberOfRays = 20;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private BehaviorParameters behaviorParameters;


    private void Awake()
    {
        behaviorParameters.BrainParameters.VectorObservationSize = numberOfRays * (detectableTags.Count + 2) + 3;
    }

    public bool show;
    private void Update()
    {
        CastRays();
    }

    public List<float> CastRays()
    {
        List<float> finalOutput = new List<float>();

        for (int i = 0; i < numberOfRays; i++)
        {
            float rayAngle = i * angle / (numberOfRays - 1) - angle / 2;
            Vector3 rayDirection = Quaternion.Euler(0, rayAngle, 0) * transform.forward;
            Ray ray = new Ray(transform.position, rayDirection);

            List<float> oneHot = new List<float>();

            if(Physics.SphereCast(ray , sphereCastRadius , out RaycastHit hit , rayDistance))
            {
                foreach (string currentTag in detectableTags)
                {
                    oneHot.Add(hit.collider.gameObject.CompareTag(currentTag) ? 1.0f : 0.0f);

                    if (hit.collider.gameObject.CompareTag("BodyPart") && currentTag == "BodyPart")
                    {
                        Transform hitParentTransform = hit.collider.transform.parent;
                        Transform ourParentTransform = transform.parent.transform.parent;
                        if (hitParentTransform.gameObject == ourParentTransform.gameObject)
                        {
                            oneHot[oneHot.Count - 1] = 0f;
                        }
                    }
                }

                oneHot.Add(0f); //No collision is false
                oneHot.Add(hit.distance / rayDistance);
            }
            else
            {
                foreach (string tag in detectableTags)
                {
                    oneHot.Add(0f);
                }
                oneHot.Add(1f); //No collision is true
                oneHot.Add(0f);
            }

            finalOutput.AddRange(oneHot);
        }
        return finalOutput;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < numberOfRays; i++)
        {
            float rayAngle = i * angle / (numberOfRays - 1) - angle / 2;
            Vector3 rayDirection = Quaternion.Euler(0, rayAngle, 0) * transform.forward;
            Ray ray = new Ray(transform.position, rayDirection);
            Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
            Gizmos.DrawWireSphere(ray.origin + ray.direction * rayDistance, sphereCastRadius);
        }
    }

    private void DebugName(List<float> list)
    {
        Debug.Log(list.Count);
        List<string> stringList = list.ConvertAll(f => f.ToString());
        string listString = string.Join(", ", stringList);
        Debug.Log(listString);
    }
}
