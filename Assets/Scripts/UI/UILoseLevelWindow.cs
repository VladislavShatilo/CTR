using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UILoseLevelWindow : UIBaseLevelWindow
{
    [Header("UI")]
    [SerializeField] private Button quitButton;

    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI powerText;

    private void Start()
    {
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(restartButton, nameof(restartButton), this);
        ComponentValidator.CheckAndLog(powerText, nameof(powerText), this);

        quitButton.onClick.AddListener(OnQuit);
        restartButton.onClick.AddListener(OnRestart);
    }
    private void OnQuit()
    {
        YG2.InterstitialAdvShow();
        StartCoroutine(OnQuitCoroutine());
    }
    private void OnRestart()
    {
        YG2.InterstitialAdvShow();

        StartCoroutine(OnRestartCoroutine());
    }
    private void OnDestroy()
    {
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    public void UpdatePowerText(int power)
    {
        powerText.text = power.ToString();
    }
}