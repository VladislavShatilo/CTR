using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI window for displaying advertisements with close and watch reward options
/// </summary>
public class UIAdvWindow : UIWindow
{
    [Header("UI")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button watchAdvButton;

    [Header("Settings")]
    [SerializeField] private float closeDelay = 0.35f;
    void Start()
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

    /// <summary>
    /// Handles close button click
    /// </summary>
    private void OnClose()
    {
        StartCoroutine(OnCloseCor());
    }

    /// <summary>
    /// Handles watch advertisement button click
    /// </summary>
    private void OnReward()
    {

    }

}
