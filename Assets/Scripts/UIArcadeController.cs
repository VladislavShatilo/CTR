using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;


public class UIArcadeController : MonoBehaviour
{
    public static UIArcadeController Instance { get; private set; }

    [Header("Windows")]
    [SerializeField] private GameObject pauseWindowGO;
    [SerializeField] private GameObject finalWindowGO;
    [SerializeField] private GameObject advWindowGO;

    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI countDownInGameText;
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private TextMeshProUGUI scoreInGameText;
    [SerializeField] private TextMeshProUGUI moneyInGameText;
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
    [SerializeField] private TextMeshProUGUI coinsInMenuText;
    [SerializeField] private TextMeshProUGUI starsInMenuText;
    [SerializeField] private TextMeshProUGUI scoreInMenuText;
    [SerializeField] private Button settingInMenuOnButton;
    [SerializeField] private Button settingInMenuCloseButton;
    [SerializeField] private Button infoInMenuOnButton;
    [SerializeField] private Button infoInMenuCloseButton;

    [Header("Final Window UI")]
    [SerializeField] private TextMeshProUGUI scoreInFinalWindowText;
    [SerializeField] private TextMeshProUGUI moneyInFinalWindowText;
    [SerializeField] private Button quitInFinalWindowButton;
    [SerializeField] private Button restartInFinalWindowButton;

    [Header("References")]
    [SerializeField] private PlayerObstacleHandler playerObstacleHandler;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        playerObstacleHandler.OnCrash += ShowFinalWindow;
    }


   
    public void HideAllGameWindows()
    {
        PauseWindowGO.SetActive(false);
        FinalWindowGO.SetActive(false);
        AdvWindowGO.SetActive(false);

    }
    public void SetMenuStatistic()
    {
        starsInMenuText.text = Storage.Instance.stars.ToString("N0");
        coinsInMenuText.text = Storage.Instance.money.ToString("N0");
        scoreInMenuText.text = Storage.Instance.score.ToString("N0");
        YG2.SetLeaderboard("score", Storage.Instance.score);
    }
    public void ShowFinalWindow()
    {
        StartCoroutine(ShowFinalWindowCor());
    }
    private IEnumerator ShowFinalWindowCor()
    {
        yield return new WaitForSeconds(0.7f);
        int score = int.Parse(UIArcadeController.Instance.ScoreInGameText.text, System.Globalization.NumberStyles.AllowThousands);

        if (!Storage.Instance.isRewardArcadeShown && Storage.Instance.canShowArcadeRewardTime && score > 700 * Storage.Instance.carMultiplier[Storage.Instance.SelectedCar])
        {

            finalWindowGO.SetActive(true);
            FindObjectOfType<WindowAnimation>().ToggleMenuOn("ResultWindow");
            Storage.Instance.isRewardArcadeShown = true;

        }
        else
        {
            finalWindowGO.SetActive(true);
            FindObjectOfType<WindowAnimation>().ToggleMenuOn("ResultWindow");
            //smoothScore.CountScore();
        }

    }
    public void ShowFinalAdvWindow()
    {
        advWindowGO.SetActive(true);
        FindObjectOfType<WindowAnimation>().ToggleMenuOn("AdWindow");
    }

    public GameObject PauseWindowGO => pauseWindowGO;
    public GameObject FinalWindowGO => finalWindowGO;
    public GameObject AdvWindowGO => advWindowGO;
    public TextMeshProUGUI CountDownInGameText => countDownInGameText;
    public Canvas MenuCanvas => menuCanvas;
    public Canvas InGameCanvas => inGameCanvas;
    public TextMeshProUGUI CoinsInMenuText => coinsInMenuText;
    public TextMeshProUGUI StarsInMenuText => starsInMenuText;
    public TextMeshProUGUI ScoreInMenuText => scoreInMenuText;
    public TextMeshProUGUI ScoreInGameText => scoreInGameText;
    public TextMeshProUGUI MoneyInGameText => moneyInGameText;
    public TextMeshProUGUI ScoreInFinalWindowText => scoreInFinalWindowText;
    public TextMeshProUGUI MoneyInFinalWindowText => moneyInFinalWindowText;
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
