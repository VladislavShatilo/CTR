using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;


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
     private Vector3 lowPosition;
     private Vector3 highPosition;
    private Vector3 visiblePosition;
    [SerializeField] private float duration = 0.5f;

    private void Start()
    {
        float screenHeight = Screen.height * 1.5f;
        lowPosition = new Vector3(0, -screenHeight, 0);
        highPosition = new Vector3(0, screenHeight, 0);

        if (menus.Length > 0)
        {
            float elementHeight = menus[0].menu.rect.height;
            if (elementHeight > Screen.height)
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
        UIArcadeController controller = UIArcadeController.Instance;
        controller.SettingInMenuOnButton.onClick.AddListener(delegate { ToggleMenuOn("SettingsWindow"); });
        controller.SettingInMenuCloseButton.onClick.AddListener(delegate { ToggleMenuOff("SettingsWindow", 0); });
        controller.InfoInMenuOnButton.onClick.AddListener(delegate { ToggleMenuOn("InfoGameWindow"); });
        controller.InfoInMenuCloseButton.onClick.AddListener(delegate { ToggleMenuOff("InfoGameWindow", 0); });
        controller.PauseInGameOnButton.onClick.AddListener(delegate { ToggleMenuOn("PauseWindow"); });
        controller.PauseContinueButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow",(int)PauseOptions.Continue); });
        controller.PauseRestartButton.onClick.AddListener(delegate { ShowMidgame(); });
        controller.PauseRestartButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Restart); });
        controller.PauseQuitButton.onClick.AddListener(delegate { ShowMidgame(); });
        controller.PauseQuitButton.onClick.AddListener(delegate { ToggleMenuOff("PauseWindow", (int)PauseOptions.Quit); });
        controller.AdvCloseButton.onClick.AddListener(delegate { ToggleMenuOff("AdWindow", 0); });
        controller.ResultRestartBtn.onClick.AddListener(delegate { ShowMidgame(); });
        controller.ResultRestartBtn.onClick.AddListener(delegate { ToggleMenuOff("ResultWindow",(int)FinalOptions.Restart); });
        controller.ResultQuitBtn.onClick.AddListener(delegate { ShowMidgame(); });
        controller.ResultQuitBtn.onClick.AddListener(delegate { ToggleMenuOff("ResultWindow", (int)FinalOptions.Quit); });
    }

    public void ShowMidgame()
    {
        YG2.InterstitialAdvShow();
    }
    public void CloseAdvWindow()
    {
        ToggleMenuOff("AdWindow", 0);
    
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
        else
        {
            Debug.Log("null");

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
             
                manager.ContinueGame();
                break;
            case 1:
               // YG2.InterstitialAdvShow();

                manager.RestartGame();
                break;
            case 2:
                //  FindObjectOfType<BackToMainMenu>().Arcade1(); 
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               // YG2.InterstitialAdvShow();

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
                //YG2.InterstitialAdvShow();
                Storage.Instance.isRewardArcadeShown = false;
                manager.RestartGame();
                break;
            case 1:
                //YG2.InterstitialAdvShow();
                Storage.Instance.isRewardArcadeShown = false;
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
