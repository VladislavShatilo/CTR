using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ScreenManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private LevelSwitch portraitLevelSwitch;

    [SerializeField] private LevelSwitch landscapeLevelSwitch;
    [SerializeField] private Canvas portraitCanvas;
    [SerializeField] private Canvas landscapeCanvas;

    [Header("Settings")]
    [SerializeField] private float aspectRatioThreshold = 1f;

    private void Start()
    {
        ValidateReferences();
        UpdateLayout();
    }

    private void ValidateReferences()
    {
        ComponentValidator.CheckAndLog(portraitLevelSwitch, nameof(portraitLevelSwitch), this);
        ComponentValidator.CheckAndLog(landscapeLevelSwitch, nameof(landscapeLevelSwitch), this);
        ComponentValidator.CheckAndLog(portraitCanvas, nameof(portraitCanvas), this);
        ComponentValidator.CheckAndLog(landscapeCanvas, nameof(landscapeCanvas), this);
    }

    public void UpdateLayout()
    {
        bool isPortrait = GetAspectRatio() < aspectRatioThreshold;

        if (portraitLevelSwitch != null) portraitLevelSwitch.gameObject.SetActive(isPortrait);
        if (landscapeLevelSwitch != null) landscapeLevelSwitch.gameObject.SetActive(!isPortrait);
        if (portraitCanvas != null) portraitCanvas.gameObject.SetActive(isPortrait);
        if (landscapeCanvas != null) landscapeCanvas.gameObject.SetActive(!isPortrait);
    }

    private float GetAspectRatio()
    {
        return (float)Screen.width / Screen.height;
    }
}