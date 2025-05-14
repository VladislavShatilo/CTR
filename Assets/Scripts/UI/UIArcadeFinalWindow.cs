using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIArcadeFinalWindow : UIBaseArcadeWindow
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private float durationAnimation = 1f;

    private int currentScore;
    private int currentMoney;
    private float timer;
 
    void Start()
    {
     
        if (quitButton == null || restartButton == null || scoreText == null || coinsText== null) 
        {
            Debug.LogError("Buttons are missing");
            enabled = false;
        }
        quitButton.onClick.AddListener(OnQuit);
        restartButton.onClick.AddListener(OnRestart);

    }
    private void OnQuit() => StartCoroutine(OnQuitCor());
    private void OnRestart() => StartCoroutine(OnRestartCor());

    private void OnDestroy()
    {
        quitButton.onClick.RemoveListener(OnQuit);
        restartButton.onClick.RemoveListener(OnRestart);
    }


    private void ButtonsEnable(bool enable)
    {
        quitButton.gameObject.SetActive(enable);
        restartButton.gameObject.SetActive(enable);
    }

    public void CountScore()
    {
        ButtonsEnable(false);
        StartCoroutine(UpdateScoreCoroutine());
    }

   

    IEnumerator UpdateScoreCoroutine()
    {
        currentScore = 0;
        timer = 0f;
        if(UIArcadeManager.Instance == null)
        {
            Debug.LogError(" UIArcadeManager.Instance is missing");
            enabled = false;
        }
       
        var arcadeHUD = UIArcadeManager.Instance.ArcadeHUD;


        int targetScore = GetTargetScore(arcadeHUD.ScoreText);
        int targetCoins = GetTargetScore(arcadeHUD.CoinsText);
        while (timer < durationAnimation)
        {
            float progress = timer / durationAnimation;
            float smoothProgress = Mathf.SmoothStep(0, 1, progress);

            currentScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, smoothProgress));
            currentMoney = Mathf.RoundToInt(Mathf.Lerp(0, targetCoins, smoothProgress));

            scoreText.text = FormatNumber(currentScore);
            coinsText.text = FormatNumber(currentMoney);
            timer += Time.deltaTime;
            yield return null;
        }

        currentScore = targetScore;
        currentMoney = targetCoins;
        scoreText.text = FormatNumber(currentScore);
        coinsText.text = FormatNumber(currentMoney);
        ButtonsEnable(true);

    }
    private int  GetTargetScore(TextMeshProUGUI textField)
    {
        if (textField == null)
        {
            Debug.LogError("Text is missing");
            enabled = false;

        }
        return int.Parse(textField.text, System.Globalization.NumberStyles.AllowThousands);
    }
    private string FormatNumber(int number)
    {
        if (number >= 1_000_000)
        {
            return (number / 1_000_000f).ToString("0.##") + "m";
        }
        if (number >= 1_000)
        {
            return (number / 1_000f).ToString("0.##") + "k";
        }
        return number.ToString();
    }
}
