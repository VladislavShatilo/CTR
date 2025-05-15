using UnityEngine;
using System.Collections;
using DG.Tweening.Core.Easing;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class FinalBuff : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float powerForLevel = 60f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float delayBetweenAnimations = 0.3f;

    [Header("References")]
    [SerializeField] private ParticleSystem[] confettiEffects;
    [SerializeField] private Sprite filledStarSprite;
   

    [Header("Season Settings")]
    [SerializeField] private int levelsPerSeason = 12;
    [SerializeField] private int[] seasonStarsThresholds = { 25, 52 };
    [SerializeField] private int[] seasonRewards = { 200, 500, 2000 }; // Пример значений

    private UIFinalLevelWindow finalWindow;
    private int currentLevelIndex;
    private bool isInitialized;
    private PlayerMove player;
    private UILevelManager uiLevelManager;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (isInitialized) return;

        if (!int.TryParse(SceneManager.GetActiveScene().name, out int levelNumber))
        {
            Debug.LogError("Invalid scene name format!", this);
            enabled = false;
            return;
        }
        ComponentValidator.CheckAndLog(UILevelManager.Instance, nameof(UILevelManager.Instance), this);

        uiLevelManager = UILevelManager.Instance;
        ComponentValidator.CheckAndLog(PlayerMove.Instance, nameof(PlayerMove.Instance), this);
        player = PlayerMove.Instance;
        currentLevelIndex = levelNumber - 1;
        ComponentValidator.CheckAndLog(UILevelManager.Instance.Windows, nameof(UILevelManager.Instance.Windows), this);
        finalWindow = UILevelManager.Instance.Windows.GetWindow<UIFinalLevelWindow>();
        finalWindow.SetActiveButtons(false);

        enabled = false;
        isInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody.TryGetComponent(out PlayerMove _))
            return;

        if (!isInitialized) Initialize();

        StartFinalSequence();
    }

    private void StartFinalSequence()
    {
        //player.enabled = false;
        FindObjectOfType<Gamemanager>().StopCamera();

        PlayConfettiEffects();

        UpdateLevelProgress();
        StartCoroutine(FinalSequenceCoroutine());
    }

    private IEnumerator FinalSequenceCoroutine()
    {
        uiLevelManager.LevelHUD.AllUIActive(false);
        yield return new WaitForSeconds(1f);

        uiLevelManager.Windows.ShowWindow<UIFinalLevelWindow>();

        // Анимация мощности
        yield return AnimateValue(
            startValue: 0,
            endValue: uiLevelManager.LevelHUD.GetCurrentPower(),
            updateAction: value => finalWindow.UpdatePowerText((int)value)
        );

        yield return new WaitForSeconds(delayBetweenAnimations);

        // Показ звезд
        int starsEarned = CalculateStarsEarned();
        yield return finalWindow.ShowStars(starsEarned, filledStarSprite);

        // Анимация монет (если уровень не был пройден ранее)
        if (Storage.Instance.levelsCompleted[currentLevelIndex] == 1)
        {
            int reward = CalculateLevelReward();
            yield return AnimateValue(
                startValue: 0,
                endValue: reward,
                updateAction: value => finalWindow.UpdateCoinsText((int)value)
            );
            Storage.Instance.coins += reward;
            Storage.Instance.Save();
        }
        else
        {
            finalWindow.UpdateCoinsText(0);
        }

        // Проверка блокировки кнопки "Далее"
        if (IsLastLevelInSeason() && !HasEnoughSeasonStars())
        {
            finalWindow.BlockNextButtonOnLastLevel();
        }

        finalWindow.SetActiveButtons(true);
    }

    private int CalculateStarsEarned()
    {
        float progress = Mathf.Clamp01(uiLevelManager.LevelHUD.GetCurrentPower() / powerForLevel);

        if (progress >= 0.66f) return 3;
        if (progress >= 0.33f) return 2;
        return 1;
    }

    private void UpdateLevelProgress()
    {
        int stars = CalculateStarsEarned();
        Storage.Instance.levelsCompleted[currentLevelIndex] = 1;
        Storage.Instance.levelsStars[currentLevelIndex] = stars;
        Storage.Instance.Save();

    }

    private int CalculateLevelReward()
    {
        int seasonIndex = currentLevelIndex / levelsPerSeason;
        return seasonIndex < seasonRewards.Length ? seasonRewards[seasonIndex] : 0;
    }

    private bool IsLastLevelInSeason()
    {
        return (currentLevelIndex + 1) % levelsPerSeason == 0;
    }

    private bool HasEnoughSeasonStars()
    {
        int seasonIndex = (currentLevelIndex + 1) / levelsPerSeason - 1;
        return seasonIndex >= 0 &&
               seasonIndex < seasonStarsThresholds.Length &&
               Storage.Instance.totalStars >= seasonStarsThresholds[seasonIndex];
    }

    private void PlayConfettiEffects()
    {
        foreach (var effect in confettiEffects)
        {
            effect?.Play();
        }
    }

    private IEnumerator AnimateValue(float startValue, float endValue, System.Action<float> updateAction)
    {
        float timer = 0f;
        while (timer < animationDuration)
        {
            float progress = timer / animationDuration;
            float currentValue = Mathf.Lerp(startValue, endValue, progress);
            updateAction?.Invoke(currentValue);

            timer += Time.deltaTime;
            yield return null;
        }

        updateAction?.Invoke(endValue);
    }
}