using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoWindow : UIWindow
{
    [SerializeField] private Button closeButton;
    void Start()
    {
        closeButton.onClick.AddListener(OnClose);
    }
    private void OnClose()
    {
        CloseWindow();
        UIManager.Instance.Windows.GetWindow<UISettingsWindow>().BGSetTrue();
    }
}
