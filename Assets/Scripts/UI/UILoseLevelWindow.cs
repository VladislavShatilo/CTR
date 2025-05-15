using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILoseLevelWindow : UIBaseLevelWindow
{
    [Header("UI")]
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI powerText;

    void Start()
    {
        ComponentValidator.CheckAndLog(quitButton, nameof(quitButton), this);
        ComponentValidator.CheckAndLog(restartButton, nameof(restartButton), this);
        ComponentValidator.CheckAndLog(powerText, nameof(powerText), this);

        quitButton.onClick.AddListener(()=>StartCoroutine(OnQuitCoroutine()));
        restartButton.onClick.AddListener(() => StartCoroutine(OnRestartCoroutine()));
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
