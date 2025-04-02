using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeBuffTimer : MonoBehaviour
{
    [SerializeField] private buffScripts buffScripts;
    [SerializeField] private float countdownTime = 5f;

    public bool isTimerActive = false;

    private Text timerText;
    private Image buffImage;
    private float currentTime = 0f;
    private bool isCountingDown = false;
    private string mode;

    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        buffImage = GetComponentInChildren<Image>();
        EnableTimer(false);

    }

    private void EnableTimer(bool enabled)
    {
        timerText.enabled = enabled;
        buffImage.enabled = enabled;
        isCountingDown = enabled;
        isTimerActive = enabled;
    }

    void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;               
                EnableTimer(false);

                if (mode.Equals("IMMORTALITY"))
                {
                    buffScripts.DeactivateImmortality();
                }
                else if (mode.Equals("COINS"))
                {
                    buffScripts.DeactivateDoubledCoins();
                }
            }

            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer(String buff, Sprite buffIMG)
    {
        mode = buff;
        buffImage.sprite = buffIMG;
        currentTime = countdownTime;
        EnableTimer(true);
    }

    public void PauseTimer()
    {
        isCountingDown = false;
    }

    public void ContinueTimer()
    {
        if(isTimerActive)
            isCountingDown = true;
    }
    public void StopTimer()
    {
        EnableTimer(false);    
    }

    public void ResetTimer()
    {
        currentTime = countdownTime;
        UpdateTimerText();
    }
}
