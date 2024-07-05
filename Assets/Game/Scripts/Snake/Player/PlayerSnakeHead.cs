using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeHead : SnakeHead
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); 
    }
}
