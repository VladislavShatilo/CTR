using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIArcadeHUDManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button pauseButton;

    [Header("Referencies")]
    [SerializeField] private ArcadeManager arcadeManager;

    private WaitForSecondsRealtime countdownDelay;
    private WaitForSecondsRealtime countdownInterval;


    private void Awake()
    {
        ComponentValidator.CheckAndLog(hudCanvas, nameof(hudCanvas), this);
        ComponentValidator.CheckAndLog(countDownText, nameof(countDownText), this);
        ComponentValidator.CheckAndLog(scoreText, nameof(scoreText), this);
        ComponentValidator.CheckAndLog(coinsText, nameof(coinsText), this);
        ComponentValidator.CheckAndLog(pauseButton, nameof(pauseButton), this);
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance, nameof(UIArcadeManager.Instance), this);

        countdownDelay = new WaitForSecondsRealtime(0.2f);
        countdownInterval = new WaitForSecondsRealtime(0.7f);
    }
    public void Initialize()
    {
        pauseButton.onClick.AddListener(OnPauseClicked);
        countDownText.gameObject.SetActive(false);

    }
    private void OnDestroy()
    {
        pauseButton.onClick.RemoveListener(OnPauseClicked);
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("N0");
    }
 
    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString("N0");
    }

    
    public IEnumerator StartCountdown()
    {
        yield return countdownDelay;

        countDownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return countdownInterval;
        }

        countDownText.gameObject.SetActive(false);

    }
    public void SetActiveHUD(bool active)
    {
        hudCanvas.gameObject.SetActive(active);
    }

    private void OnPauseClicked()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);
        ComponentValidator.CheckAndLog(arcadeManager, nameof(arcadeManager), this);

        UIArcadeManager.Instance.Windows.ShowWindow<UIPauseWindow>();
        arcadeManager.PauseGame();
    }

    public TextMeshProUGUI CoinsText { get { return coinsText; } }
    public TextMeshProUGUI ScoreText { get { return scoreText; } }


}
