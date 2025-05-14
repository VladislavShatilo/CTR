using System.Collections;
using System.Collections.Generic;
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
        if(UIMenuCanvas == null)
        {
            LogAndDisable("Menu Canvas reference is missing!");

        }
        if(levelMenuButton == null || arcadeButton == null|| shopButton == null|| settingsButton == null)
        {
            LogAndDisable("One or more button references are missing!");

        }
        if (coinsText == null || starsText == null || recordText == null)
        {
            LogAndDisable("One or more button references are missing!");

        }
        if (transition == null || arcadeManager == null || audioScript == null)
        {
            LogAndDisable("Transition reference is missing!");
        }
    }
    private void LogAndDisable(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
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
        starsText.text = Storage.Instance.stars.ToString();
        recordText.text = Storage.Instance.score.ToString();
        YG2.SetLeaderboard("score", Storage.Instance.score);
    }
    private void OnLevelMenuClicked()
    {
        transition.LoadScenebByName("LevelMenu");
    }
    private void OnArcadeClicked()
    {
        UIMenuCanvas.gameObject.SetActive(false);
        arcadeManager.StartArcadeFromMenu();
        audioScript.SetInGameMusicON();
    }
    private void OnShopClicked()
    {
        transition.LoadScenebByName("Shop");

    }
    private void OnSettingsClicked()
    {
        if(UIArcadeManager.Instance.Windows != null)
        {
            UIArcadeManager.Instance.Windows.ShowWindow<UISettingsWindow>();

        }
        else
        {
            Debug.LogError("IArcadeManager.Instance.Windows is missing");

        }
    }


}
