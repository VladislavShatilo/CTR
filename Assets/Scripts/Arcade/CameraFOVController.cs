using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFOVController : MonoBehaviour
{
    [Header("FOV Settings")]
    [SerializeField] private float defaultFOV = 60f;

    [SerializeField] private float fovChangeSpeed = 5f;

    private Camera cameraArcade;

    private void Awake()
    {
        cameraArcade = Camera.main;
        ComponentValidator.CheckAndLog(cameraArcade, nameof(cameraArcade), this);
    }

    private void LateUpdate()
    {
        HandleFOV();
    }

    private void HandleFOV()
    {
        cameraArcade.fieldOfView = Mathf.Lerp(
            cameraArcade.fieldOfView,
            defaultFOV,
            fovChangeSpeed * Time.deltaTime
        );
    }
}