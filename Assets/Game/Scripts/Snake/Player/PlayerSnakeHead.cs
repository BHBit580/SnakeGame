using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeHead : SnakeHead
{
    [SerializeField] private VoidEventChannelSO playerDied;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); 
    }

    private void OnDestroy()
    {
        playerDied?.RaiseEvent();
    }
}
