using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseLevelWindow : UIBaseLevelWindow
{
    [Header("UI")]
    [SerializeField] private Button quitButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;

    [Header("Referencies")]
    [SerializeField] private Gamemanager gameManager;


    void Start()
    {
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(resumeButton, nameof(resumeButton), this);
        ComponentValidator.CheckAndLog(restartButton, nameof(restartButton), this);
        ComponentValidator.CheckAndLog(gameManager, nameof(gameManager), this);

        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCoroutine()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCoroutine()));
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
        gameManager.PauseFunction(false);

    }

 
}
