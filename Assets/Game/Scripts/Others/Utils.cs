using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : MonoBehaviour
{
    [SerializeField] private Collider spawnArea;
    public CoolDownSystem CoolDownSystem;


    public Vector2 GetRandomSpawnPositionInsideSpawnArea()
    {
        Bounds bounds = spawnArea.bounds;
        float positionX = Random.Range(-45, 35);
        float positionZ = Random.Range(-45, 45);


        return new Vector2(positionX, positionZ);
    }
}
