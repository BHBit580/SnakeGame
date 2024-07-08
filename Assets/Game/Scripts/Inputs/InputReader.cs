using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerInputs.INewactionmapActions
{
    public float HorizontalInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    
    private PlayerInputs playerInputs;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInputs = new PlayerInputs();
        playerInputs.Newactionmap.SetCallbacks(this); 
        playerInputs.Newactionmap.Enable();
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<float>();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        Vector2 rawMousePosition = context.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(rawMousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            MouseWorldPosition = hit.point;
        }
    }

    private void OnDestroy()
    {
        playerInputs.Newactionmap.Disable();
    }
}
