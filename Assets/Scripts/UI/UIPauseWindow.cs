using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseWindow : UIBaseArcadeWindow
{
    [Header("UI")]
    [SerializeField] private Button quitButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;

    void Start()
    {
        if(quitButton == null || resumeButton == null || restartButton == null)
        {
            Debug.LogError("Buttons are missing");
            enabled = false;
        }
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
        if (UIArcadeManager.Instance.Windows == null)
        {
            Debug.LogError("UIArcadeManager.Instance.Windows is missing");
            enabled = false;
        }
        else
        {
            UIArcadeManager.Instance.Windows.HideTopWindow();

        }
        StartCoroutine(ResumeCorutine());

    }

    private IEnumerator ResumeCorutine()
    {
        if(UIArcadeManager.Instance.ArcadeHUD == null)
        {
            Debug.LogError("UIArcadeManager.Instance.ArcadeHUD is missing");
            enabled = false;
        }
        yield return UIArcadeManager.Instance.ArcadeHUD.StartCountdown();
        arcadeManager.ContinueGame();

    }

}
