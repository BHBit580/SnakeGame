using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : MonoBehaviour
{
    [SerializeField] private Collider spawnArea;
    public CoolDownSystem CoolDownSystem;


    public Vector2 GetRandomSpawnPositionInsideSpawnArea()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector2(x, z);
    }
}
