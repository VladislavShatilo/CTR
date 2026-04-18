using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas UIMenuCanvas;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button levelMenuButton;
    [SerializeField] private Button arcadeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;

    [Header("Referenceis")]
    [SerializeField] private Transition transition;

    [SerializeField] private ArcadeManager arcadeManager;
    [SerializeField] private AudioScript audioScript;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(UIMenuCanvas, nameof(UIMenuCanvas), this);
        ComponentValidator.CheckAndLog(levelMenuButton, nameof(levelMenuButton), this);
        ComponentValidator.CheckAndLog(arcadeButton, nameof(arcadeButton), this);
        ComponentValidator.CheckAndLog(shopButton, nameof(shopButton), this);
        ComponentValidator.CheckAndLog(settingsButton, nameof(settingsButton), this);
        ComponentValidator.CheckAndLog(coinsText, nameof(coinsText), this);
        ComponentValidator.CheckAndLog(starsText, nameof(starsText), this);
        ComponentValidator.CheckAndLog(recordText, nameof(recordText), this);
        ComponentValidator.CheckAndLog(transition, nameof(transition), this);
        ComponentValidator.CheckAndLog(arcadeManager, nameof(arcadeManager), this);
        ComponentValidator.CheckAndLog(audioScript, nameof(audioScript), this);
    }
    private void Start()
    {
        UpdateStatesInfo();
    }
    private void OnEnable()
    {
        YG2.onOpenAnyAdv += PauseGameAdv;
        YG2.onCloseAnyAdv += ResumeGameAdv;
    }
    private void OnDisable()
    {
        YG2.onOpenAnyAdv -= PauseGameAdv;
        YG2.onCloseAnyAdv -= ResumeGameAdv;
    }

    private void PauseGameAdv()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;

    }
    private void ResumeGameAdv()
    {
        Time.timeScale = 1f;
        AudioListener.volume = Storage.Instance.volume;

    }

    public void Initialize()
    {
        levelMenuButton.onClick.AddListener(delegate { OnLevelMenuClicked(); });
        arcadeButton.onClick.AddListener(delegate { OnArcadeClicked(); });
        shopButton.onClick.AddListener(delegate { OnShopClicked(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsClicked(); });
    }

    private void OnDestroy()
    {
        levelMenuButton.onClick.RemoveAllListeners();
        arcadeButton.onClick.RemoveAllListeners();
        shopButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
    }

    public void UpdateStatesInfo()
    {
        coinsText.text = Storage.Instance.coins.ToString();
        starsText.text = Storage.Instance.totalStars.ToString();
        recordText.text = Storage.Instance.highScore.ToString();
        YG2.SetLeaderboard("score", Storage.Instance.highScore);
    }

    private void OnLevelMenuClicked()
    {
        YG2.InterstitialAdvShow();

        transition.LoadScenebByName("LevelMenu");
    }

    private void OnArcadeClicked()
    {
        YG2.InterstitialAdvShow();
        UIMenuCanvas.gameObject.SetActive(false);
        arcadeManager.StartArcadeFromMenu();
        audioScript.SetInGameMusicON();
    }

    private void OnShopClicked()
    {
        YG2.InterstitialAdvShow();

        transition.LoadScenebByName("Shop");
    }

    private void OnSettingsClicked()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);
        UIArcadeManager.Instance.Windows.ShowWindow<UISettingsWindow>();
    }
}