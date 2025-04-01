using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimation2 : MonoBehaviour
{
    enum LoseOptions
    {       
        Restart,
        Menu
    }

    enum PauseOptions
    {
        Quit,
        Restart,
        Resume
    }
    enum WinOptions
    {
        Next,
        Restart,
        Menu
    }

    [System.Serializable]
    private class Menu
    {
        public RectTransform menu;
        public string menuName;
    }
    [SerializeField] private Image TransitionImage;
    [SerializeField] private Menu[] menus;
    [SerializeField] private Vector3 lowPosition;
    [SerializeField] private Vector3 highPosition;
    [SerializeField] private Vector3 visiblePosition;
    [SerializeField] private float duration = 0.5f;

    private void Start()
    {
      
        float screenHeight = Screen.height * 1.5f;
        lowPosition = new Vector3(0, -screenHeight, 0);
        highPosition = new Vector3(0, screenHeight, 0);

        if (menus.Length > 0)
        {
            float elementWidth = menus[0].menu.rect.width;
            if (elementWidth > Screen.width)
            {
                lowPosition = new Vector3(0, -screenHeight * 1.5f, 0);
                highPosition = new Vector3(0, screenHeight * 1.5f, 0);

            }
        }
        TransitionImage.gameObject.SetActive(false);
        foreach (var menu in menus)
        {
            menu.menu.anchoredPosition = lowPosition;
        }
        UIController controller = UIController.Instance;
        controller.PauseButton.onClick.AddListener(delegate { ToggleMenuOn("PauseWindow"); });
        controller.PauseQuitButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Quit); });
        controller.PauseRestartButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Restart); });
        controller.PauseResumeButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Resume); });
        controller.MainLevelMenuButton.onClick.AddListener(delegate { ChangeSceneTransition("LevelMenu"); });

        controller.NextWinButton.onClick.AddListener(delegate { ToggleMenuOff("FinalWindow",(int)WinOptions.Next); });
        controller.RestartWinButton.onClick.AddListener(delegate { ToggleMenuOff("FinalWindow", (int)WinOptions.Restart); });
        controller.MenuWinButton.onClick.AddListener(delegate { ToggleMenuOff("FinalWindow", (int)WinOptions.Menu); });

        controller.RestartLoseButton.onClick.AddListener(delegate { ToggleMenuOff("LoseWindow", (int)LoseOptions.Restart); });
        controller.MenuLoseButton.onClick.AddListener(delegate { ToggleMenuOff("LoseWindow", (int)LoseOptions.Menu); });

    }

    public void ToggleMenuOn(string menuName)
    {
        StartCoroutine(ToggleMenuOnCor(menuName));
    }

    public void ToggleMenuOff(string menuName, int action)
    {
        StartCoroutine(ToggleMenuOffCor(menuName, action));
    }

    private IEnumerator ToggleMenuOnCor(string menuName)
    {
        yield return new WaitForSeconds(0.05f);
        var menu = GetMenu(menuName);
        menu?.menu.DOAnchorPos(visiblePosition, duration).SetEase(Ease.OutCubic);
    }

    private IEnumerator ToggleMenuOffCor(string menuName, int action)
    {
        var menu = GetMenu(menuName);
        if (menu != null)
        {
            menu.menu.DOAnchorPos(highPosition, duration).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(duration);
            GameObject.Find(menu.menuName)?.SetActive(false);
            menu.menu.anchoredPosition = lowPosition;
        }

        if (menuName == "PauseWindow")
        {
            HandlePauseMenuAction(action);
        }
        if (menuName == "FinalWindow")
        {
            HandleFinalMenuAction(action);
        }
        if (menuName == "LoseWindow")
        {
            HandleLoseMenuAction(action);
        }
    }

    private Menu GetMenu(string menuName)
    {
        foreach (var menu in menus)
        {
            if (menu.menuName == menuName)
                return menu;
        }
        return null;
    }

    private void HandlePauseMenuAction(int action)
    {
        switch (action)
        {
            case (int)PauseOptions.Quit:
                ChangeSceneTransition("LevelMenu");
                break;
            case (int)PauseOptions.Restart:
                ChangeSceneTransition(Storage.Instance.nameActiveScene);
                break;
            case (int)PauseOptions.Resume:
                FindObjectOfType<Gamemanager>().PauseFunction(false);
                break;
        }
    }
    private void HandleFinalMenuAction(int action)
    {

        switch (action)
        {
            case (int)WinOptions.Next:
                ChangeSceneTransition((int.Parse(Storage.Instance.nameActiveScene) + 1).ToString());
                break;
            case (int)WinOptions.Restart:
                ChangeSceneTransition(Storage.Instance.nameActiveScene);
                break;
            case (int)WinOptions.Menu:
                ChangeSceneTransition("LevelMenu");

                break;
        
        }
    }
    private void HandleLoseMenuAction(int action)
    {

        switch (action)
        {        
            case (int)LoseOptions.Restart:
                Debug.Log("132");
                ChangeSceneTransition(Storage.Instance.nameActiveScene);
                break;
            case (int)LoseOptions.Menu:
                ChangeSceneTransition("LevelMenu");
                break;

        }
    }

    private void ChangeSceneTransition(string sceneName)
    {
        StartCoroutine(ChangeSceneTransitionCor(sceneName));
    }

    private IEnumerator ChangeSceneTransitionCor(string sceneName)
    {
        TransitionImage.gameObject.SetActive(true);
        var Smooth = TransitionImage.GetComponent<SmoothTransition>();        
        StartCoroutine(Smooth.StartCor());       
        yield return new WaitForSeconds(0.7f);
        SceneAddressManager.Instance.LoadScene(sceneName);
    }
}
