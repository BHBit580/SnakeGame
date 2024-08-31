using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerInputs.INewactionmapActions
{
    [SerializeField] private float dis = 0.5f;
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
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 mousePos = new Vector2(MouseWorldPosition.x, MouseWorldPosition.z);

        Debug.Log(Vector2.Distance(playerPos , mousePos));

        if (Vector2.Distance(playerPos, mousePos) < dis)
        {
            MouseWorldPosition = _mousePos;
        }
        else
        {
            _mousePos = MouseWorldPosition;
        }
    }

    private Vector3 _mousePos;

    private void OnDestroy()
    {
        playerInputs.Newactionmap.Disable();
    }
}
