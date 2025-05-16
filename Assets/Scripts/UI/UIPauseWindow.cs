using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseWindow : UIBaseArcadeWindow
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
        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCor()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCor()));
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    private void OnResume()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);
        UIArcadeManager.Instance.Windows.HideTopWindow();
        StartCoroutine(ResumeCorutine());
    }

    private IEnumerator ResumeCorutine()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.ArcadeHUD, nameof(UIArcadeManager.Instance.ArcadeHUD), this);
        yield return UIArcadeManager.Instance.ArcadeHUD.StartCountdown();
        arcadeManager.ContinueGame();
    }
}