using System.Collections.Generic;
using UnityEngine;

public interface ISnake
{
    void AddBodyPart(Transform newPart);
    List<Transform> GetSnakeBeadsList();
}