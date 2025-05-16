using UnityEngine;
using UnityEngine.UI;

public class UISettingsWindow : UIWindow
{
    [Header("UI elements")]
    [SerializeField] private Button closeButton;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button openInfoButton;
    [SerializeField] private Image BGImage;

    private void Start()
    {
        ComponentValidator.CheckAndLog(closeButton, nameof(closeButton), this);
        ComponentValidator.CheckAndLog(volumeSlider, nameof(volumeSlider), this);
        ComponentValidator.CheckAndLog(openInfoButton, nameof(openInfoButton), this);
        ComponentValidator.CheckAndLog(BGImage, nameof(BGImage), this);

        closeButton.onClick.AddListener(CloseArcadeWindow);
        openInfoButton.onClick.AddListener(OnOpenInfo);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        openInfoButton.onClick.RemoveAllListeners();
    }

    private void OnOpenInfo()
    {
        CloseArcadeWindow();
        BGImage.gameObject.SetActive(false);
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);
        UIArcadeManager.Instance.Windows.ShowWindow<UIInfoWindow>();
    }

    public void BGSetTrue()
    {
        BGImage.gameObject.SetActive(true);
    }
}