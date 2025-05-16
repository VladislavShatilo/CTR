using UnityEngine;

public class PointsCounter : MonoBehaviour, IArcadeStateListener
{
    [Header("Settings")]
    [SerializeField] private float scoreMultiplier = 0.2f / 150f;

    [SerializeField] private RoadGenerator roadGenerator;

    private float currentScore = 0f;
    private float currentSpeedFactor = 1f;
    private bool isCounting = false;
    private UIArcadeHUDManager hudManager;

    private const int SCORE_DISPLAY_MULTIPLIER = 100;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(roadGenerator, nameof(roadGenerator), this);
    }

    private void Start()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.ArcadeHUD, nameof(UIArcadeManager.Instance.ArcadeHUD), this);
        hudManager = UIArcadeManager.Instance.ArcadeHUD;
        currentScore = 0;
        isCounting = true;
        UpdateCounterText();
    }

    private void Update()
    {
        if (!isCounting)
        {
            return;
        }

        CalculateScore();
        UpdateCounterText();
    }

    private void CalculateScore()
    {
        float carMultiplier = Storage.Instance.carMultiplier[Storage.Instance.selectedCar];
        currentSpeedFactor = roadGenerator.speed * scoreMultiplier;
        currentScore += Time.deltaTime * currentSpeedFactor * carMultiplier;
    }

    private void UpdateCounterText()
    {
        int displayScore = Mathf.RoundToInt(currentScore * SCORE_DISPLAY_MULTIPLIER);
        hudManager.UpdateScore(displayScore);
    }

    public void OnArcadePaused() => StopCounter();

    public void OnArcadeContinued() => StartCounter();

    public void OnArcadeRestart() => RestartCounter();

    private void StartCounter() => isCounting = true;

    private void StopCounter() => isCounting = false;

    public void RestartCounter()
    {
        isCounting = true;
        currentScore = 0f;
        UpdateCounterText();
    }
}