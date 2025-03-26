using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WindowAnimation : MonoBehaviour
{    
   enum PauseOptions
    {
        Continue,
        Restart,
        Quit
    }
    enum FinalOptions
    {
        Restart,
        Quit
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
        TransitionImage.gameObject.SetActive(false);
        foreach (var menu in menus)
        {
            menu.menu.anchoredPosition = lowPosition;
        }
        UIArcadeController controller = UIArcadeController.Instance;
        controller.SettingInMenuOnButton.onClick.AddListener(delegate { ToggleMenuOn("Settings"); });
        controller.SettingInMenuCloseButton.onClick.AddListener(delegate { ToggleMenuOff("Settings",0); });
        controller.InfoInMenuOnButton.onClick.AddListener(delegate { ToggleMenuOn("AboutGameWindow"); });
        controller.InfoInMenuCloseButton.onClick.AddListener(delegate { ToggleMenuOff("AboutGameWindow",0); });
        controller.PauseInGameOnButton.onClick.AddListener(delegate { ToggleMenuOn("PauseWindow"); });
        controller.PauseContinueButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow",(int)PauseOptions.Continue); });
        controller.PauseRestartButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Restart); });
        controller.PauseQuitButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Quit); });
        controller.AdvCloseButton.onClick.AddListener(delegate { ToggleMenuOff("AdWindow", 0); });
        controller.ResultRestartBtn.onClick.AddListener(delegate { ToggleMenuOff("ResultWindow",(int)FinalOptions.Restart); });
        controller.ResultQuitBtn.onClick.AddListener(delegate { ToggleMenuOff("ResultWindow", (int)FinalOptions.Quit); });
    }

    public void ToggleMenuOn(string menuName)
    {
        StartCoroutine(ToggleMenuOnCor(menuName));
    }

    public void ToggleMenuOff(string menuName,int action)
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
        if (menuName == "AdWindow")
        {
            UIArcadeController.Instance.AdvWindowBGImage.gameObject.SetActive(false);

        }
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
        if (menuName == "ResultWindow")
        {
            HandleResultMenuAction(action);
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
        var manager = FindObjectOfType<ArcadeManager>();
        switch (action)
        {
            case 0:
                Debug.Log("HandlePauseMenuAction1");
                manager.ContinueGame();
                break;
            case 1:
                manager.RestartGame();
                break;
            case 2:
                //  FindObjectOfType<BackToMainMenu>().Arcade1(); 
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                BackToMenu();

                break;
        }
    }
    private void HandleResultMenuAction(int action)
    {
        var manager = FindObjectOfType<ArcadeManager>();

        switch (action)
        {
            case 0:
                manager.RestartGame();
                break;
            case 1:
               
                BackToMenu();
                break;
            
        }
    }

    private void BackToMenu()
    {
        StartCoroutine(BackToMenuCor());
    }
    private IEnumerator BackToMenuCor()
    {
        TransitionImage.gameObject.SetActive(true);
        var Smooth = TransitionImage.GetComponent<SmoothTransition>();
 
            StartCoroutine(Smooth.StartCor());

        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Arcade");
    }
}
