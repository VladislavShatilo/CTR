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
        if(quitButton == null || resumeButton == null || restartButton == null)
        {
            LogErrorAndEnabled("Buttons are missing");
        }
        if(gameManager == null)
        {
            LogErrorAndEnabled("GameManager is missing");
        }

        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCoroutine()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCoroutine()));
    }
    private void LogErrorAndEnabled(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
    }
    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();

    }
    private void OnResume()
    {
        if (UILevelManager.Instance.Windows == null)
        {
            LogErrorAndEnabled("UILevelManager.Instance.Windows is missing");
        }
        else
        {
            UILevelManager.Instance.Windows.HideTopWindow();

        }

        gameManager.PauseFunction(false);

    }

 
}
