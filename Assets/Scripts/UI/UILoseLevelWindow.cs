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
        if(quitButton == null || restartButton == null || powerText == null)
        {
            Debug.LogError("UI is missing");
            enabled = false;
        }
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
