using UnityEngine;

public class Learn : MonoBehaviour
{
    public float range = 2f;
    public float enemyDetectionRadius = 1.5f;
    public float speed = 2f;
    public float rotationSpeed = 4f;
    public float avoidanceAngle = 15f; 

   

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
        DetectAndAvoidObstacles();
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
    }

    private void DetectAndAvoidObstacles()
    {
        float currentAngle = 0f;
        int attempts = 0;

        while (true) 
        {
            if (!CheckForObstacle(currentAngle))
            {
                RotateHead(Quaternion.Euler(0, currentAngle, 0) * transform.forward);
                break;
            }

            attempts++;
            if (attempts % 2 == 1)
            {
                // Odd attempts: check left side
                currentAngle += avoidanceAngle * attempts;
            }
            else
            {
                // Even attempts: check right side
                currentAngle = -avoidanceAngle * attempts;
            }
        }
    }

    private bool CheckForObstacle(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
        Ray ray = new Ray(transform.position, direction);

        return Physics.SphereCast(ray, enemyDetectionRadius, range, LayerMask.GetMask("BodyPart"), QueryTriggerInteraction.Collide);
    }

    private void RotateHead(Vector3 targetDirection)
    {
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDirection).eulerAngles.y, 0);
    }
}