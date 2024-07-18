using Unity.MLAgents;
using UnityEngine;

public class SimpleHead : MonoBehaviour
{
    [SerializeField] private Agent agent;
    [SerializeField] private Material winMaterial , looseMaterial;
    [SerializeField] private MeshRenderer ground;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            ground.material = winMaterial;
            agent.SetReward(3);
            Destroy(other.gameObject);
            agent.EndEpisode();
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            ground.material = looseMaterial;
            agent.SetReward(-2);
            agent.EndEpisode();
        }
    }
}
