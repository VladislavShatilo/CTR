using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShakeEffect : MonoBehaviour
{

    [Header("Shake Settings")]
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.3f;

    private float shakeTimer;
    private Vector3 originalPosition;
    private Camera cameraArcade;
    private void Awake()
    {
        cameraArcade = Camera.main;
        ComponentValidator.CheckAndLog(cameraArcade, nameof(cameraArcade), this);
    }

    void LateUpdate()
    {
        HandleShake();
    }
    private void HandleShake()
    {
        originalPosition = transform.localPosition;
        if (shakeTimer > 0)
        {
            transform.localPosition = originalPosition + UnityEngine.Random.insideUnitSphere * shakeIntensity;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake() => shakeTimer = shakeDuration;


}
