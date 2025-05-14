using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoWindow : UIWindow
{
    [SerializeField] private Button closeButton;
    void Start()
    { 
        if (closeButton == null)
        {
            Debug.LogError("closeButton is missing"); 
            enabled = false;
        }
        closeButton.onClick.AddListener(OnClose);
    }
    private void OnClose()
    {
        CloseArcadeWindow();
        if (UIArcadeManager.Instance.Windows.GetWindow<UISettingsWindow>() != null)
        {
            UIArcadeManager.Instance.Windows.GetWindow<UISettingsWindow>().BGSetTrue();

        }
        else
        {
            Debug.LogError("UISettingsWindow problem");
            enabled = false;
        }
    }
}
