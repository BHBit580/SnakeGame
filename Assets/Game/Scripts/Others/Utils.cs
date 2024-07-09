using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : GenericSingleton<Utils>
{
    [SerializeField] private Collider spawnArea;
    public CoolDownSystem CoolDownSystem;

    public Vector2 GetRandomSpawnPositionInsideSpawnArea()
    {
        Bounds bounds = spawnArea.bounds;
        float positionX = Random.Range(bounds.min.x, bounds.max.x);
        float positionZ = Random.Range(bounds.min.z , bounds.max.z);

        return new Vector2(positionX, positionZ);
    }
}
