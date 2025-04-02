using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("Play Menu UI")]
    [SerializeField] private Button playButton;
    [SerializeField] private Text tapToPlayText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Image levelBGImage;
    [SerializeField] private Text levelText;
    [SerializeField] private Image powerBGImage;
    [SerializeField] private Text powerText;
    [SerializeField] private Image powerImage;
    [SerializeField] private Button mainLevelMenuButton;

    [Header("Lose Window UI")]
    [SerializeField] private GameObject loseWindowGO;
    [SerializeField] private Text losePowerTextOnWindow;
    [SerializeField] private Button restartLoseButton;
    [SerializeField] private Button menuLoseButton;

    [Header("Pause Window UI")]
    [SerializeField] private Button pauseQuitButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseResumeButton;

    [Header("Win Window UI")]
    [SerializeField] private GameObject winWindowGO;
    [SerializeField] private Image [] starOnWinWindowImages;
    [SerializeField] private Button nextWinButton;
    [SerializeField] private Button restartWinButton;
    [SerializeField] private Button menuWinButton;
    [SerializeField] private Text finalPowerWinText;
    [SerializeField] private Text levelCoinsWinText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }

    public void UIMenuGameOff()
    {
        pauseButton.gameObject.SetActive(false);
        levelBGImage.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        powerBGImage.gameObject.SetActive(false);
        powerText.gameObject.SetActive(false);
        powerImage.gameObject.SetActive(false);

    }

    public void UIMenuGameHide(bool boolValue)
    {
        pauseButton.gameObject.SetActive(boolValue);
        powerBGImage.gameObject.SetActive(boolValue);
        powerImage.gameObject.SetActive(boolValue);
        powerText.gameObject.SetActive(boolValue);

    }

    public void LoseWindowAnimation()
    {
        StartCoroutine(LoseWindowAnimationCor());
    }
    public IEnumerator LoseWindowAnimationCor()
    {
        yield return new WaitForSeconds(0.15f);   
        loseWindowGO.SetActive(true);
        FindObjectOfType<WindowAnimation2>().ToggleMenuOn("LoseWindow");
    }

    public void SetLosePowerText()
    {
        losePowerTextOnWindow.text = powerText.text;
    }

    public Button PlayButton => playButton;
    public Text TapToPlayText => tapToPlayText;
    public Button PauseButton => pauseButton;
    public Text LevelText => levelText;
    public Text PowerText => powerText;
    public Image PowerImage => powerImage;
    public Button MainLevelMenuButton => mainLevelMenuButton;  
    public Button RestartLoseButton => restartLoseButton;
    public Button MenuLoseButton => menuLoseButton;

    public Button PauseQuitButton => pauseQuitButton;
    public Button PauseRestartButton => pauseRestartButton;
    public Button PauseResumeButton => pauseResumeButton;

    public Button NextWinButton => nextWinButton;
    public Button RestartWinButton => restartWinButton;
    public Button MenuWinButton => menuWinButton;
    public Text FinalPowerWinText => finalPowerWinText;
    public Text LevelCoinsWinText => levelCoinsWinText;
    public GameObject WinWindow => winWindowGO;
    public Image[] StarOnWinWindowImages => starOnWinWindowImages;

}
