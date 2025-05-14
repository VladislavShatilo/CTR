using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBaseLevelWindow : UIWindow
{
    [Header("Base References")]
    [SerializeField] protected Transition transition;

    [Header("Transition settings")]
    [SerializeField, Range(0.1f, 2f)] private float quitToMenuDelay = 0.7f;
    [SerializeField, Range(0.1f, 2f)] private float restartLevelDelay = 0.7f;

    private void Start()
    {
        if(transition == null)
        {
            Debug.LogError("Transition is missing");
            enabled = false;
        }
    }
    protected IEnumerator OnQuitCoroutine()
    {
        CloseLevelWindow();
        yield return new WaitForSeconds(quitToMenuDelay);
        transition.LoadScenebByName("LevelMenu");
    }

    protected IEnumerator OnRestartCoroutine()
    {
        CloseLevelWindow();
        yield return new WaitForSeconds(restartLevelDelay);
        string currentSceneName = SceneManager.GetActiveScene().name;
        transition.LoadScenebByName(currentSceneName);
    }
}
