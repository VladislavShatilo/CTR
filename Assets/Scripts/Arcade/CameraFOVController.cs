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

    private Camera cameraArcade;
    private void Awake()
    {
        cameraArcade = Camera.main;
        ComponentValidator.CheckAndLog(cameraArcade, nameof(cameraArcade), this);
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
