using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoWindow : UIWindow
{
    [SerializeField] private Button closeButton;
    void Start()
    {
        ComponentValidator.CheckAndLog(closeButton, nameof(closeButton), this);
        closeButton.onClick.AddListener(OnClose);
    }
    private void OnClose()
    {
        CloseArcadeWindow();
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows.GetWindow<UISettingsWindow>(),
            nameof(UIArcadeManager.Instance.Windows), this);

        UIArcadeManager.Instance.Windows.GetWindow<UISettingsWindow>().BGSetTrue();
    }
}
