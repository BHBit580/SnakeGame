using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

[RequireComponent(typeof(DecisionRequester))]
public class EnemyAgent : Agent , ISnake
{
    [Header("References")]
    [SerializeField] private CustomRaycast3D customRaycast3D;


    [Header("Values")]
    [SerializeField] private List<Transform> bodyPartsList = new List<Transform>();
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float speed = 5f;


    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    private Transform headTransform;


    private void Awake()
    {
        headTransform = transform.GetChild(0);
        if(transform.localPosition != Vector3.zero) Debug.LogError("PLz make it zero due to locality");
        if(bodyPartsList[0] == null) Debug.LogError("Kindly add head to 0th index");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(headTransform.localPosition);
        sensor.AddObservation(customRaycast3D.CastRays());
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.ContinuousActions;
        float moveRotate = action[0];

        headTransform.Translate(headTransform.forward * (speed * Time.deltaTime) , Space.World);
        headTransform.Rotate(0f , moveRotate * speed , 0f , Space.Self);
        MoveBodyParts();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = (int)Input.GetAxis("Horizontal");
    }

    private void MoveBodyParts()
    {
        for (int i = 1; i < bodyPartsList.Count; i++)
        {
            curBodyPart = bodyPartsList[i];
            PrevBodyPart = bodyPartsList[i - 1];

            dis = Vector3.Distance(PrevBodyPart.position,curBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            newpos.y = headTransform.position.y;

            float T = Time.deltaTime * dis / minDistance * speed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart(Transform newPart)
    {
        bodyPartsList.Add(newPart);
    }

    public List<Transform> GetSnakeBeadsList()
    {
        return bodyPartsList;
    }

    private void OnDestroy()
    {
        EndEpisode();
    }
}
