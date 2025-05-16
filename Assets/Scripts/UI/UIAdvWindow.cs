using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAdvWindow : UIWindow
{
    [Header("UI")]
    [SerializeField] private Button closeButton;

    [SerializeField] private Button watchAdvButton;

    [Header("Settings")]
    [SerializeField] private float closeDelay = 0.35f;

    private void Start()
    {
        ComponentValidator.CheckAndLog(closeButton, nameof(closeButton), this);
        ComponentValidator.CheckAndLog(watchAdvButton, nameof(watchAdvButton), this);

        closeButton.onClick.AddListener(OnClose);
        watchAdvButton.onClick.AddListener(OnReward);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(OnClose);
        watchAdvButton.onClick.RemoveListener(OnReward);
    }

    private IEnumerator OnCloseCor()
    {
        CloseArcadeWindow();
        yield return new WaitForSeconds(closeDelay);

        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);

        var windowManager = UIArcadeManager.Instance.Windows;
        windowManager.ShowWindow<UIArcadeFinalWindow>();
        windowManager.GetWindow<UIArcadeFinalWindow>().CountScore();
    }

    private void OnClose()
    {
        StartCoroutine(OnCloseCor());
    }

    private void OnReward()
    {
    }
}