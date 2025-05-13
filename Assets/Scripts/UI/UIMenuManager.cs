using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas UIMenuCanvas;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button levelMenuButton;
    [SerializeField] private Button arcadeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;
    public void Initialize()
    {
        levelMenuButton.onClick.AddListener(delegate { OnLevelMenuClicked(); });
        arcadeButton.onClick.AddListener(delegate { OnArcadeClicked(); });
        shopButton.onClick.AddListener(delegate { OnShopClicked(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsClicked(); });
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
        
    }
    private void OnArcadeClicked()
    {
        UIMenuCanvas.gameObject.SetActive(false);
        ArcadeManager.Instance.StartArcadeFromMenu();
        
        AudioScript.Instance.SetInGameMusicON();
    }
    private void OnShopClicked()
    {
        
    }
    private void OnSettingsClicked()
    {
        UIManager.Instance.Windows.ShowWindow<UISettingsWindow>();
    }


}
