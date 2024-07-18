using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : MonoBehaviour
{
    [SerializeField] private Collider spawnArea;
    public CoolDownSystem CoolDownSystem;


    public Vector2 GetRandomSpawnPositionInsideSpawnArea()
    {
        Bounds bounds = spawnArea.bounds;
        float positionX = Random.Range(25, -25);
        float positionZ = Random.Range(30 , -25);

        return new Vector2(positionX, positionZ);
    }
}
