using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsWindow : UIWindow
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button openInfoButton;
    [SerializeField] private Image BGImage;
    void Start()
    {
        closeButton.onClick.AddListener(CloseWindow);
        openInfoButton.onClick.AddListener(OnOpenInfo);
    }


    private void OnOpenInfo()
    {
        CloseWindow();
        BGImage.gameObject.SetActive(false);
        UIManager.Instance.Windows.ShowWindow<UIInfoWindow>();
    }

    public void BGSetTrue()
    {
        BGImage.gameObject.SetActive(true);
    }

}
