using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Controls camera intro animation with smooth transition and parenting to target.
/// </summary>
[RequireComponent(typeof(Camera))]
public class ArcadeCameraController : MonoBehaviour
{

    [Header("Intro Animation")]
    [SerializeField] private bool enableIntro = true;
    [SerializeField, Min(0.1f)] private float introDuration = 3f;
    [SerializeField] private Vector3 introTargetPosition = new Vector3(0, 24, -25);
    [SerializeField] private Vector3 introTargetRotation = new Vector3(26, 0, 0);
    [SerializeField] private AnimationCurve introMovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Transform introTarget;
    private Camera cameraArcade;

    private void Awake()
    {
        cameraArcade = GetComponent<Camera>();
        if(cameraArcade == null)
        {
            Debug.LogError("Camera is null");
            enabled = false;
        }
    }

    /// <summary>
    /// Starts the intro animation sequence
    /// </summary>
    /// <param name="target">Target transform to follow after intro</param>
    public void PlayIntroAnimation(Transform playerTransform)
    {
        if (!enableIntro) return;

        introTarget = playerTransform;
        if (introTarget == null)
        {
            Debug.LogError("IntroTarget is null");
            enabled = false;
        }

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
                introTarget.position + introTargetPosition,
                progress
            ), Quaternion.Slerp(
                startRot,
                targetRot,
                progress
            ));

            yield return null;
        }

        cameraArcade.gameObject.transform.SetParent(introTarget);       
     
    }  
}
