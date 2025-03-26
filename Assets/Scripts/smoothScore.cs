using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class smoothScore : MonoBehaviour
{
    [SerializeField] private float durationInSeconds = 1f;

    private TextMeshProUGUI scoreInFinalWindowText;
    private TextMeshProUGUI moneyInFinalWindowText;
    private int currentScore;
    private int currentMoney;
    private float timer;

   
    private void ButtonsInFinalWindowEnable(bool enable)
    {
        UIArcadeController.Instance.QuitInFinalWindowButton.gameObject.SetActive(enable);
        UIArcadeController.Instance.RestartInFinalWindowButton.gameObject.SetActive(enable);
    }

    public void CountScore()
    {
        gameObject.SetActive(true);
        FindObjectOfType<WindowAnimation>().ToggleMenuOn("ResultWindow");
        ButtonsInFinalWindowEnable(false);
        currentScore = 0;
        timer = 0f;
        StartCoroutine(UpdateScoreCoroutine());
    }

    IEnumerator UpdateScoreCoroutine()
    {
        scoreInFinalWindowText = UIArcadeController.Instance.ScoreInFinalWindowText;
        moneyInFinalWindowText = UIArcadeController.Instance.MoneyInFinalWindowText;
        int targetScore = int.Parse(UIArcadeController.Instance.ScoreInGameText.text, System.Globalization.NumberStyles.AllowThousands);
        int targetMoney = int.Parse(UIArcadeController.Instance.MoneyInGameText.text, System.Globalization.NumberStyles.AllowThousands);
        while (timer < durationInSeconds)
        {
            float progress = timer / durationInSeconds;
            float smoothProgress = Mathf.SmoothStep(0, 1, progress);

            currentScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, smoothProgress));
            currentMoney = Mathf.RoundToInt(Mathf.Lerp(0, targetMoney, smoothProgress));

            scoreInFinalWindowText.text = FormatNumber(currentScore);
            moneyInFinalWindowText.text = FormatNumber(currentMoney);
            timer += Time.deltaTime;
            yield return null;
        }

        currentScore = targetScore;
        currentMoney = targetMoney;
        scoreInFinalWindowText.text = FormatNumber(currentScore);
        moneyInFinalWindowText.text = FormatNumber(currentMoney);
        ButtonsInFinalWindowEnable(true);

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
