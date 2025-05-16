using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelHUDManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button pauseButton;

    [SerializeField] private Button quitButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI tapToPlayText;
    [SerializeField] private Image powerBGImage;
    [SerializeField] private Image powerImage;
    [SerializeField] private Image levelBGImage;

    [Header("Referencies")]
    [SerializeField] private Transition transition;

    private const string LEVEL_PREFIX = "Level ";

    private void Awake()
    {
        ComponentValidator.CheckAndLog(pauseButton, nameof(pauseButton), this);
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(playButton, nameof(playButton), this);
        ComponentValidator.CheckAndLog(powerText, nameof(powerText), this);
        ComponentValidator.CheckAndLog(levelText, nameof(levelText), this);
        ComponentValidator.CheckAndLog(tapToPlayText, nameof(tapToPlayText), this);
        ComponentValidator.CheckAndLog(powerBGImage, nameof(powerBGImage), this);
        ComponentValidator.CheckAndLog(powerImage, nameof(powerImage), this);
        ComponentValidator.CheckAndLog(levelBGImage, nameof(levelBGImage), this);
        ComponentValidator.CheckAndLog(transition, nameof(transition), this);
    }

    public void Initialize()
    {
        UpdateLevelText(SceneManager.GetActiveScene().name);

        pauseButton.onClick.AddListener(OnPause);
        quitButton.onClick.AddListener(OnQuit);
        playButton.onClick.AddListener(OnPlay);

        SetGameplayUI(false);
        playButton.gameObject.SetActive(true);
        tapToPlayText.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveListener(OnPause);
        quitButton.onClick.RemoveListener(OnQuit);
        playButton.onClick.RemoveListener(OnPlay);
    }

    public void UpdateLevelText(string levelName)
    {
        if (int.TryParse(levelName, out int levelNumber))
        {
            levelText.text = $"{LEVEL_PREFIX}{levelNumber}";
        }
        else
        {
            levelText.text = $"{LEVEL_PREFIX}{levelName}";
            Debug.LogWarning($"Failed to parse level number from: {levelName}", this);
        }
    }

    private void OnPause()
    {
        ComponentValidator.CheckAndLog(UILevelManager.Instance.Windows, nameof(UILevelManager.Instance.Windows), this);
        UILevelManager.Instance.Windows.ShowWindow<UIPauseLevelWindow>();

        Storage.Instance.isPauseGlobal = true;
        PlayerMove.Instance.enabled = false;
    }

    private void OnQuit()
    {
        transition.LoadScenebByName("LevelMenu");
    }

    private void OnPlay()
    {
        playButton.gameObject.SetActive(false);
        tapToPlayText.gameObject.SetActive(false);
        PlayerMove.Instance.enabled = false;
        quitButton.gameObject.SetActive(false);
        SetGameplayUI(true);
        PlayerMove.Instance.enabled = true;
    }

    public void SetGameplayUI(bool enable)
    {
        pauseButton.gameObject.SetActive(enable);
        powerBGImage.gameObject.SetActive(enable);
        powerImage.gameObject.SetActive(enable);
        powerText.gameObject.SetActive(enable);
    }

    public void AllUIActive(bool enable)
    {
        SetGameplayUI(enable);
        levelText.gameObject.SetActive(enable);
        levelBGImage.gameObject.SetActive(enable);
    }

    public void UpdatePowerText(int power)
    {
        powerText.text = power.ToString();
    }

    public int GetCurrentPower()
    {
        int power = 0;
        try
        {
            power = int.Parse(powerText.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Parse failed: {e.Message}", this);
        }
        return power;
    }
}