using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraResolution : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetObject;

    [Header("Portrait Settings")]
    [SerializeField] private float portraitScale = 0.015f;

    [SerializeField] private Vector3 portraitTargetScale = new Vector3(0.6f, 0.6f, 0.6f);

    [Header("Landscape Settings")]
    [SerializeField] private float landscapeScale = 0.02f;

    [SerializeField] private Vector3 landscapeTargetScale = Vector3.one;

    [Header("Advanced")]
    [SerializeField] private float adjustmentSpeed = 5f;

    [SerializeField] private bool updateContinuously = false;

    private float currentScale;
    private Vector3 currentTargetScale;
    private float aspectRatioThreshold = 1f;

    private void Awake()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!", this);
            enabled = false;
            return;
        }
        ComponentValidator.CheckAndLog(targetObject, nameof(targetObject), this);

        ApplyInitialScale();
    }

    private void Update()
    {
        if (updateContinuously)
        {
            AdjustScale();
        }
    }

    private void ApplyInitialScale()
    {
        bool isLandscape = IsLandscape();
        currentScale = isLandscape ? landscapeScale : portraitScale;
        currentTargetScale = isLandscape ? landscapeTargetScale : portraitTargetScale;

        transform.localScale = Vector3.one * currentScale;
        targetObject.localScale = currentTargetScale;
    }

    private void AdjustScale()
    {
        bool isLandscape = IsLandscape();
        float targetScale = isLandscape ? landscapeScale : portraitScale;
        Vector3 targetObjectScale = isLandscape ? landscapeTargetScale : portraitTargetScale;

        currentScale = Mathf.Lerp(currentScale, targetScale, adjustmentSpeed * Time.deltaTime);
        currentTargetScale = Vector3.Lerp(currentTargetScale, targetObjectScale, adjustmentSpeed * Time.deltaTime);

        transform.localScale = Vector3.one * currentScale;
        targetObject.localScale = currentTargetScale;
    }

    private bool IsLandscape()
    {
        return (float)Screen.width / Screen.height > aspectRatioThreshold;
    }
}