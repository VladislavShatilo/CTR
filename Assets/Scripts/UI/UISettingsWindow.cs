using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsWindow : UIWindow
{
    [Header("UI elements")]

    [SerializeField] private Button closeButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button openInfoButton;
    [SerializeField] private Image BGImage;
    void Start()
    {
        if(closeButton == null || volumeSlider == null || openInfoButton == null || BGImage == null)
        {
            Debug.LogError("UI elements are missing");
            enabled = false;
        }
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
        if(UIArcadeManager.Instance.Windows == null)
        {
            Debug.LogError("UIArcadeManager.Instance.Windows is missing");
            enabled = false;
        }
        else
        {
            UIArcadeManager.Instance.Windows.ShowWindow<UIInfoWindow>();

        }
    }

    public void BGSetTrue()
    {
        BGImage.gameObject.SetActive(true);
    }

}
