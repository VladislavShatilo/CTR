using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera field of view with smooth transitions
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraFOVController : MonoBehaviour
{
    [Header("FOV Settings")]
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private float fovChangeSpeed = 5f;
    // Start is called before the first frame update
    private Camera cameraArcade;
    private void Awake()
    {
        cameraArcade = Camera.main;
        if (cameraArcade == null)
        {
            Debug.LogError("Camera is null");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        HandleFOV();
    }

    /// <summary>
    /// Smoothly adjusts camera FOV towards target value
    /// </summary>
    private void HandleFOV()
    {
        cameraArcade.fieldOfView = Mathf.Lerp(
            cameraArcade.fieldOfView,
            defaultFOV,
            fovChangeSpeed * Time.deltaTime
        );
    }

}
