using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFinalLevelWindow : UIBaseLevelWindow
{
    [Header("UI Elements")]
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Image [] starImages;

    [Header("Animation Settings")]
    [SerializeField] private float starAppearDelay = 0.2f;
    [SerializeField] private float finalDelayAfterStars = 0.3f;
    [SerializeField] private float nextLevelDelay = 0.7f;

    void Start()
    {
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(restartButton, nameof(restartButton), this);
        ComponentValidator.CheckAndLog(nextLevelButton, nameof(nextLevelButton), this);
        ComponentValidator.CheckAndLog(powerText, nameof(powerText), this);
        ComponentValidator.CheckAndLog(coinsText, nameof(coinsText), this);

        quitButton.onClick.AddListener(() => StartCoroutine(OnQuitCoroutine()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCoroutine()));
        nextLevelButton.onClick.AddListener(() => StartCoroutine(OnNextLevelCoroutine()));

        InitializeStars();
    }
    private void InitializeStars()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            ComponentValidator.CheckAndLog(starImages[i], nameof(starImages), this);
           // starImages[i].gameObject.SetActive(false);
            
        }
    }
    private IEnumerator OnNextLevelCoroutine()
    {
        CloseLevelWindow();
        yield return new WaitForSeconds(nextLevelDelay);
        if (int.TryParse(SceneManager.GetActiveScene().name, out int currentLevel))
        {
            string nextLevel = (currentLevel + 1).ToString();
            transition.LoadScenebByName(nextLevel);
           
        }
        else
        {
            Debug.LogError("Failed to parse current scene name");
        }
    }
    public void UpdatePowerText(int power)
    {
        powerText.text = power.ToString();
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void SetActiveButtons(bool enabled)
    {
        quitButton.gameObject.SetActive(enabled);
        restartButton.gameObject.SetActive(enabled);
        nextLevelButton.gameObject.SetActive(enabled);
    }

    public void BlockNextButtonOnLastLevel()
    {
        nextLevelButton.enabled = false;
        var colors = nextLevelButton.colors;
        colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        nextLevelButton.colors = colors;
    }

    public IEnumerator ShowStars(int starsEarned, Sprite starSprite )
    {
        ComponentValidator.CheckAndLog(starSprite, nameof(starSprite), this);
       
        switch (starsEarned)
        {
            case 1:
                starImages[0].sprite = starSprite;

                break;
            case 2:
                starImages[0].sprite = starSprite;
                yield return new WaitForSeconds(starAppearDelay);

                starImages[1].sprite = starSprite;

                break;
            case 3:
                starImages[0].sprite = starSprite;
                yield return new WaitForSeconds(starAppearDelay);

                starImages[1].sprite = starSprite;
                yield return new WaitForSeconds(starAppearDelay);

                starImages[2].sprite = starSprite;
                break;
        }
        yield return new WaitForSeconds(finalDelayAfterStars);


    }

    private void OnDestroy()
    {
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.RemoveAllListeners();
    }

}
