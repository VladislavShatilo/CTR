using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class ArcadeCameraController : MonoBehaviour
{

    [Header("FOV Settings")]
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private float fovChangeSpeed = 5f;

    [Header("Shake Settings")]
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.3f;

    [Header("Intro Animation")]
    [SerializeField] private bool enableIntro = true;
    [SerializeField] private float introDuration = 3f;
    [SerializeField] private Vector3 introTargetPosition = new Vector3(0, 24, -25);
    [SerializeField] private Vector3 introTargetRotation = new Vector3(26, 0, 0);
    [SerializeField] private AnimationCurve introMovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Transform inroTarget;
    private Camera cameraArcade;
    private float shakeTimer;
    private bool isIntroPlaying;

    public event Action OnIntroCompleted;
    private void Awake()
    {
        cameraArcade = Camera.main;
    }
 

  
    private void LateUpdate()
    {
        if (isIntroPlaying || inroTarget == null) return;
        HandleFOV();

    }

    public void PlayIntroAnimation(Transform playerTransform)
    {
        Debug.Log("Intromi");
        if (!enableIntro) return;

        inroTarget = playerTransform;
        isIntroPlaying = true;

        StartCoroutine(IntroAnimationRoutine());
    }
    private IEnumerator IntroAnimationRoutine()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(introTargetRotation);

        while (elapsed < introDuration)
        {
            elapsed += Time.deltaTime;
            float progress = introMovementCurve.Evaluate(elapsed / introDuration);

            transform.SetPositionAndRotation(Vector3.Lerp(
                startPos,
                inroTarget.position + introTargetPosition,
                progress
            ), Quaternion.Slerp(
                startRot,
                targetRot,
                progress
            ));

            yield return null;
        }

        isIntroPlaying = false;
        OnIntroCompleted?.Invoke();
        cameraArcade.gameObject.transform.SetParent(inroTarget);


     
    }

    private void HandleFOV()
    {
        cameraArcade.fieldOfView = Mathf.Lerp(
            cameraArcade.fieldOfView,
            defaultFOV,
            fovChangeSpeed * Time.deltaTime
        );
    }

    private void HandleShake()
    {
        if (shakeTimer > 0)
        {
           // transform.localPosition = originalPosition + UnityEngine.Random.insideUnitSphere * shakeIntensity;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
           // transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake() => shakeTimer = shakeDuration;


}
