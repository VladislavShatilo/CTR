using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIPauseLevelWindow : UIBaseLevelWindow
{
    [Header("UI")]
    [SerializeField] private Button quitButton;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(resumeButton, nameof(resumeButton), this);
        ComponentValidator.CheckAndLog(restartButton, nameof(restartButton), this);

        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuitPauseCoroutine);
        restartButton.onClick.AddListener(OnRestartPauseCoroutine);
    }
    private void OnQuitPauseCoroutine()
    {
        YG2.InterstitialAdvShow();
        StartCoroutine(OnQuitCoroutine());
        Storage.Instance.isPauseGlobal = false;

    }
    private void OnRestartPauseCoroutine()
    {
        YG2.InterstitialAdvShow();

        StartCoroutine(OnRestartCoroutine());
        Storage.Instance.isPauseGlobal = false;

    }
    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    private void OnResume()
    {
        ComponentValidator.CheckAndLog(UILevelManager.Instance.Windows, nameof(UILevelManager.Instance.Windows), this);
        UILevelManager.Instance.Windows.HideTopWindow();
        Storage.Instance.isPauseGlobal = false;
        PlayerMove.Instance.enabled = true;
        Storage.Instance.isPauseGlobal = false;
    }
}