using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

[RequireComponent(typeof(DecisionRequester))]
public class EnemyAgent : Agent
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Utils utils;



    public override void OnEpisodeBegin()
    {
        Vector2 position = utils.GetRandomSpawnPositionInsideSpawnArea();
        headTransform.localPosition = new Vector3(position.x, headTransform.localPosition.y, position.y);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(headTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.ContinuousActions;
        float moveRotate = action[0];

        headTransform.Translate(headTransform.forward * (speed * Time.deltaTime) , Space.World);
        headTransform.Rotate(0f , moveRotate * speed , 0f , Space.Self);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = (int)Input.GetAxis("Horizontal");
    }


}
