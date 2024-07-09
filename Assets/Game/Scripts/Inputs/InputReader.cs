using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerInputs.INewactionmapActions
{
    public bool IsFastSpeed { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    
    private PlayerInputs playerInputs;
    private Camera mainCamera;
    private Vector2 currentRawMousePosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInputs = new PlayerInputs();
        playerInputs.Newactionmap.SetCallbacks(this); 
        playerInputs.Newactionmap.Enable();
    }
    
    public void OnFastSpeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsFastSpeed = true;
        }
        else if (context.canceled)
        {
            IsFastSpeed = false;
        }
    }
    
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        //Unfortunately this function is only called when the mouse is moving, but we want Raycast in every frame
        currentRawMousePosition = context.ReadValue<Vector2>();
    }
    
    private void Update()
    {
        DoRaycasting(currentRawMousePosition);
    }

    private void DoRaycasting(Vector2 rawMousePosition)
    {
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
