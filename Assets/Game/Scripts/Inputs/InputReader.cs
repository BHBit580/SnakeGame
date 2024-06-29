using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerInputs.INewactionmapActions
{
    public float HorizontalInput { get; private set; }
    
    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Newactionmap.SetCallbacks(this); 
        playerInputs.Newactionmap.Enable();
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<float>();
    }

    private void OnDestroy()
    {
        playerInputs.Newactionmap.Disable();
    }
}
