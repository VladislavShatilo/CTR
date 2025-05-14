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
        if (closeButton == null || watchAdvButton == null)
        {
            Debug.LogError($"{name}: Button references are not set!", this);
            enabled = false;
        }
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
        try
        {
            var windowManager = UIArcadeManager.Instance.Windows;
            windowManager.ShowWindow<UIArcadeFinalWindow>();
            windowManager.GetWindow<UIArcadeFinalWindow>().CountScore();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to show final window: {e.Message}", this);
        }

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
