using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIArcadeController : MonoBehaviour
{
    public static UIArcadeController Instance { get; private set; }

    [Header("Windows")]
    [SerializeField] private GameObject pauseWindowGO;
    [SerializeField] private GameObject finalWindowGO;
    [SerializeField] private GameObject advWindowGO;

    [Header("In Game UI")]
    [SerializeField] private Text countDownInGameText;
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Text scoreInGameText;
    [SerializeField] private Text moneyInGameText;
    [SerializeField] private Button pauseInGameOnButton;
    [SerializeField] private Button pauseQuitButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseContinueButton;
    [SerializeField] private Button advCloseButton;
    [SerializeField] private Button resultQuitButton;
    [SerializeField] private Button resultRestartButton;
    [SerializeField] private Image advWindowBGImage;

    [Header("Main Menu UI")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Text coinsInMenuText;
    [SerializeField] private Text starsInMenuText;
    [SerializeField] private Text scoreInMenuText;
    [SerializeField] private Button settingInMenuOnButton;
    [SerializeField] private Button settingInMenuCloseButton;
    [SerializeField] private Button infoInMenuOnButton;
    [SerializeField] private Button infoInMenuCloseButton;

    [Header("Final Window UI")]
    [SerializeField] private Text scoreInFinalWindowText;
    [SerializeField] private Text moneyInFinalWindowText;
    [SerializeField] private Button quitInFinalWindowButton;
    [SerializeField] private Button restartInFinalWindowButton;

    
   

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }

    public void SetMenuStatistic()
    {
        starsInMenuText.text = Storage.Instance.stars.ToString("N0");
        coinsInMenuText.text = Storage.Instance.money.ToString("N0");
        scoreInMenuText.text = Storage.Instance.score.ToString("N0");
    }
    public void ShowFinalWindow()
    {
        advWindowGO.SetActive(true);
        FindObjectOfType<WindowAnimation>().ToggleMenuOn("AdWindow");
    }

    public GameObject PauseWindowGO => pauseWindowGO;
    public GameObject FinalWindowGO => finalWindowGO;
    public GameObject AdvWindowGO => advWindowGO;
    public Text CountDownInGameText => countDownInGameText;
    public Canvas MenuCanvas => menuCanvas;
    public Canvas InGameCanvas => inGameCanvas;
    public Text CoinsInMenuText => coinsInMenuText;
    public Text StarsInMenuText => starsInMenuText;
    public Text ScoreInMenuText => scoreInMenuText;
    public Text ScoreInGameText => scoreInGameText;
    public Text MoneyInGameText => moneyInGameText;
    public Text ScoreInFinalWindowText => scoreInFinalWindowText;
    public Text MoneyInFinalWindowText => moneyInFinalWindowText;
    public Button QuitInFinalWindowButton => quitInFinalWindowButton;
    public Button RestartInFinalWindowButton => restartInFinalWindowButton;
    public Button SettingInMenuOnButton => settingInMenuOnButton;
    public Button SettingInMenuCloseButton => settingInMenuCloseButton;
    public Button InfoInMenuOnButton => infoInMenuOnButton;
    public Button InfoInMenuCloseButton => infoInMenuCloseButton;
    public Button PauseInGameOnButton => pauseInGameOnButton;
    public Button PauseQuitButton => pauseQuitButton;
    public Button PauseRestartButton => pauseRestartButton;
    public Button PauseContinueButton => pauseContinueButton;
    public Button AdvCloseButton => advCloseButton;
    public Button ResultQuitBtn => resultQuitButton;
    public Button ResultRestartBtn => resultRestartButton;
    public Image AdvWindowBGImage => advWindowBGImage;
}
