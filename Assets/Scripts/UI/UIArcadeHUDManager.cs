using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIArcadeHUDManager : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button pauseButton;

    [Header("Referencies")]
    [SerializeField] private ArcadeManager arcadeManager;
    public void Initialize()
    {
        pauseButton.onClick.AddListener(delegate { OnPauseClicked(); });
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
 
    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }
    public void SetCountDownNumber(int number)
    {
        countDownText.text = number.ToString();
    }
    private void OnPauseClicked()
    {
        UIManager.Instance.Windows.ShowWindow<UIPauseWindow>();
        arcadeManager.PauseGame();
    }
    public IEnumerator StartCountdown()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        countDownText.gameObject.SetActive(true);
        var wait = new WaitForSecondsRealtime(0.7f);
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return wait;
        }
        countDownText.gameObject.SetActive(false);

       
    }
    public void SetActiveHUD(bool active)
    {
        hudCanvas.gameObject.SetActive(active);
    }

    public TextMeshProUGUI CoinsText { get { return coinsText; } }
    public TextMeshProUGUI ScoreText { get { return scoreText; } }


}
