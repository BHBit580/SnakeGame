using Unity.MLAgents;
using UnityEngine;


//DESTRUCTION WILL HAPPEN HERE  WE WILL HERE ONLY AND ONLY ADD SETREWARD

public class EnemyHead : SnakeHead
{
    [SerializeField] private Agent agent;
    [SerializeField] private Material winMaterial , looseMaterial;
    public MeshRenderer ground;


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Food"))
        {

            ground.material = winMaterial;
            agent.SetReward(3);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            ground.material = looseMaterial;
            agent.SetReward(-2);
            Destroy(transform.parent.gameObject);
        }

        if (other.gameObject.CompareTag("BodyPart"))
        {
            if(other.transform.parent.gameObject == transform.parent.gameObject) return;
            ground.material = looseMaterial;
            agent.SetReward(-3);
            Destroy(transform.parent.gameObject);
        }
    }
}
