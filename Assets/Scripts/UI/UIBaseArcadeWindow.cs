using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBaseArcadeWindow : UIWindow
{
    [Header("Base References")]
    [SerializeField] protected ArcadeManager arcadeManager;
    [SerializeField] protected Transition transition;

    protected IEnumerator OnQuitCor()
    {
        UIManager.Instance.Windows.HideTopWindow();
        yield return new WaitForSeconds(0.7f);
        transition.LoadScenebByName("Arcade");
    }

    protected IEnumerator OnRestartCor()
    {
        UIManager.Instance.Windows.HideTopWindow();
        yield return new WaitForSeconds(0.5f);
        arcadeManager.RestartGame();
    }
}

