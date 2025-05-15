using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
public class ArcadeBuffTimer : MonoBehaviour
{
    public event Action<BuffType> OnTimerCompleted;

    [Header("Settings")]
    [SerializeField] private float countdownTime = 5f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image buffImage;

    private bool isTimerActive = false;
    private float currentTime = 0f;
    private bool isCountingDown = false;
    private BuffType currentBuffType;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(timerText, nameof(timerText), this);
        ComponentValidator.CheckAndLog(buffImage, nameof(buffImage), this);

    }
    void Start()
    {
        EnableTimer(false);
    }

    private void EnableTimer(bool enabled)
    {
        timerText.enabled = enabled;
        buffImage.enabled = enabled;
        isCountingDown = enabled;
        isTimerActive = enabled;
        if (enabled)
        {
            UpdateTimerDisplay();
        }
            
    }

    void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0f)
            {
                CompleteTimer();
            }
        }
    }


    public void StartTimer(BuffType type, Sprite buffIcon)
    {
        currentBuffType = type;
        buffImage.sprite = buffIcon;
        currentTime = countdownTime;
        EnableTimer(true);
    }
    private void CompleteTimer()
    {
        currentTime = 0f;
        EnableTimer(false);
        OnTimerCompleted?.Invoke(currentBuffType); 
    }
    public void PauseTimer()
    {
        isCountingDown = false;
    }

    public void ContinueTimer()
    {
        if (isTimerActive)
        {
            isCountingDown = true;
        }     
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
