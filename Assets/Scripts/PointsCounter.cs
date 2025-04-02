using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private RoadGenerator roadGenerator;
    [SerializeField] private float[] carMultiplier;

    public float currentScore = 0f;

    private const float speedBalanceConst = 0.2f / 150f;
    private bool isCounting = false;

    void Start()
    {
        UIArcadeController.Instance.CountDownInGameText.enabled = true;
        currentScore = 0;
        isCounting = true;
        UpdateCounterText();
    }

    void Update()
    {
        if (isCounting)
        {
            speed = roadGenerator.speed * speedBalanceConst;

            currentScore += Time.deltaTime * speed * carMultiplier[Storage.Instance.SelectedCar];
            UpdateCounterText();
        }
    }

    void UpdateCounterText()
    {
        int scoreToShow = (int)(currentScore * 100);
        UIArcadeController.Instance.ScoreInGameText.text = scoreToShow.ToString("N0");
    }

    public void StartCounter()
    {
        isCounting = true;
    }
    public void StopCounter()
    {
        isCounting = false;
        
    }

    public void ResetCounter()
    {
        currentScore = 0f;
        UpdateCounterText();
    }
}
