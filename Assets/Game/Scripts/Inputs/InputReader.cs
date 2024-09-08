using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerInputs.INewactionmapActions
{
    public float detectionRadius = 0.5f;
    public float distance = 5f;
    public GameObject player;
    public bool IsFastSpeed { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    private PlayerInputs playerInputs;
    private Camera mainCamera;
    private Vector2 currentMouseScreenPosition;

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
        //Unfortunately this function is only called when the mouse is moving, but we want mouseScreenPosition in every frame
        currentMouseScreenPosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        DoRaycasting(currentMouseScreenPosition);
        CheckMouseOverPlayer();
    }

    private void DoRaycasting(Vector2 rawMousePosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(rawMousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            MouseWorldPosition = hit.point;
        }
    }


    private void CheckMouseOverPlayer()
    {
        Ray ray = mainCamera.ScreenPointToRay(currentMouseScreenPosition);

        RaycastHit[] hit = Physics.SphereCastAll(ray, detectionRadius, 100, Physics.AllLayers, QueryTriggerInteraction.Collide);

        List<RaycastHit> hitList = hit.ToList();

        foreach (var hitItem in hitList)
        {
            if (hitItem.collider.CompareTag("Player"))
            {
                prihit = hitItem;
                Debug.DrawRay(player.transform.position, player.transform.forward * distance, Color.red);
                MouseWorldPosition = player.transform.position + player.transform.forward * distance;

                Debug.Log(hitItem.collider.name);
            }

            if(hitItem.collider.CompareTag("Ground"))
            {
                Debug.Log(hitItem.collider.name);
            }
        }
    }

    private RaycastHit prihit;
    private void OnDrawGizmos()
    {
        if (prihit.collider != null)
        {
            Gizmos.DrawWireSphere(prihit.point , detectionRadius);
        }
    }


    private void OnDestroy()
    {
        playerInputs.Newactionmap.Disable();
    }
}
