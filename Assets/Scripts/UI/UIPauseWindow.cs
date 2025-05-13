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
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCor()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCor()));
    }
    

    private void OnResume()
    {
       UIManager.Instance.Windows.HideTopWindow();
        StartCoroutine(ResumeCorutine());

    }

    private IEnumerator ResumeCorutine()
    {
        yield return UIManager.Instance.HUD.StartCountdown();
        arcadeManager.ContinueGame();

    }

}
