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
        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCor()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCor()));

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

    
        int targetScore = int.Parse(UIManager.Instance.HUD.ScoreText.text, System.Globalization.NumberStyles.AllowThousands);
        int targetCoins = int.Parse(UIManager.Instance.HUD.CoinsText.text, System.Globalization.NumberStyles.AllowThousands);
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
