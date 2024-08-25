using Cinemachine;
using UnityEngine;

public class CameraZoomInOut : MonoBehaviour
{
    [SerializeField] private float slowSpeedFOV = 77;
    [SerializeField] private float fastSpeedFOV = 65;
    [SerializeField] private float interPolationTime = 0.5f;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;


    private void Update()
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView =
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, inputReader.IsFastSpeed ? fastSpeedFOV : slowSpeedFOV, interPolationTime * Time.deltaTime);
    }
}
