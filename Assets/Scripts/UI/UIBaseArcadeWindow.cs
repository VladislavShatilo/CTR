using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBaseArcadeWindow : UIWindow
{
    [Header("Base References")]
    [SerializeField] protected ArcadeManager arcadeManager;
    [SerializeField] protected Transition transition;

    [Header("Animation Settings")]
    [SerializeField, Range(0.1f, 2f)] private float quitDelay = 0.7f;
    [SerializeField, Range(0.1f, 2f)] private float _restartDelay = 0.5f;

    private void Start()
    {
        if (arcadeManager == null) 
        {
            Debug.LogError("arcadeManager is missing");
            enabled = false;
        }
        if (transition == null)
        {
            Debug.LogError("transition is missing");
            enabled = false;
        }
    }
    protected IEnumerator OnQuitCor()
    {
        CloseArcadeWindow();
        yield return new WaitForSeconds(quitDelay);
        
        transition.LoadScenebByName("Arcade");
    }

    protected IEnumerator OnRestartCor()
    {
        CloseArcadeWindow();
        yield return new WaitForSeconds(0.5f);
        arcadeManager.RestartGame();
    }
}

