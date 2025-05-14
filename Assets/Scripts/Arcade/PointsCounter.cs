using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour, IArcadeStateListener
{
    [SerializeField] private float speed;
    [SerializeField] private RoadGenerator roadGenerator;

    public float currentScore = 0f;

    private const float speedBalanceConst = 0.2f / 150f;
    private bool isCounting = false;
    private UIArcadeHUDManager hudManager;

    void Start()
    {
        hudManager = UIManager.Instance.HUD;
        currentScore = 0;
        isCounting = true;
        UpdateCounterText();
    }

    void Update()
    {
        if (isCounting)
        {
            speed = roadGenerator.speed * speedBalanceConst;

            currentScore += Time.deltaTime * speed * Storage.Instance.carMultiplier[Storage.Instance.SelectedCar];
          
            UpdateCounterText();
        }
    }

    void UpdateCounterText()
    {
        int scoreToShow = (int)(currentScore * 100);
        hudManager.UpdateScore(scoreToShow);
        
    }
    public void OnArcadePaused() => StopCounter();
    public void OnArcadeContinued() => StartCounter();
    public void OnArcadeRestart() => RestartCounter();
    private void StartCounter()
    {
        isCounting = true;
    }
    private void StopCounter()
    {
        isCounting = false;
        
    }

    public void RestartCounter()
    {
        isCounting = true;
        currentScore = 0f;
        UpdateCounterText();
    }
}
