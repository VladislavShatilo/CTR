using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitch : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform[] panels;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color[] backgroundColors;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;

    [Header("Animation Settings")]
    [SerializeField] private float transitionDuration = 0.5f;

    [SerializeField] private Ease transitionEase = Ease.InOutCubic;
    [SerializeField] private float buttonsDisableDuration = 0.25f;

    [Header("Offscreen Calculation")]
    [SerializeField] private float offscreenMultiplier = 1.5f;

    [SerializeField] private bool usePanelWidth = true;

    private int currentIndex = 0;
    private Vector2 centerPosition = Vector2.zero;
    private Vector2 leftOffscreen;
    private Vector2 rightOffscreen;
    private bool isTransitioning;

    private void Awake()
    {
        ValidateReferences();
        CalculateOffscreenPositions();
        InitializeNavigationButtons();
    }

    private void ValidateReferences()
    {
        ComponentValidator.CheckAndLog(panels, nameof(panels), this);
        ComponentValidator.CheckAndLog(backgroundImage, nameof(backgroundImage), this);
        ComponentValidator.CheckAndLog(backgroundColors, nameof(backgroundColors), this);
        ComponentValidator.CheckAndLog(nextButton, nameof(nextButton), this);
        ComponentValidator.CheckAndLog(backButton, nameof(backButton), this);
    }

    private void CalculateOffscreenPositions()
    {
        float screenWidth = Screen.width * offscreenMultiplier;
        float panelWidth = usePanelWidth && panels.Length > 0 ?
            panels[0].rect.width * offscreenMultiplier :
            screenWidth;

        leftOffscreen = new Vector2(-panelWidth, 0);
        rightOffscreen = new Vector2(panelWidth, 0);
    }

    private void InitializeNavigationButtons()
    {
        nextButton.onClick.AddListener(NextPanel);
        backButton.onClick.AddListener(PreviousPanel);
        UpdateNavigationButtonsState();
    }

    private void NextPanel() => StartCoroutine(TransitionCoroutine(1));

    private void PreviousPanel() => StartCoroutine(TransitionCoroutine(-1));

    private void UpdateNavigationButtonsState()
    {
        backButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < panels.Length - 1;
    }

    private IEnumerator TransitionCoroutine(int direction)
    {
        if (isTransitioning) yield break;

        isTransitioning = true;
        SetNavigationInteractable(false);

        int newIndex = (currentIndex + direction + panels.Length) % panels.Length;

        panels[currentIndex].DOAnchorPos(
            direction > 0 ? leftOffscreen : rightOffscreen,
            transitionDuration
        ).SetEase(transitionEase);

        panels[newIndex].anchoredPosition = direction > 0 ? rightOffscreen : leftOffscreen;
        panels[newIndex].DOAnchorPos(centerPosition, transitionDuration).SetEase(transitionEase);

        backgroundImage.DOColor(
     new Color(
         backgroundColors[newIndex].r,
         backgroundColors[newIndex].g,
         backgroundColors[newIndex].b,
         backgroundImage.color.a
     ),
     transitionDuration
 );

        yield return new WaitForSeconds(buttonsDisableDuration);

        currentIndex = newIndex;
        UpdateNavigationButtonsState();
        isTransitioning = false;
    }

    private void SetNavigationInteractable(bool state)
    {
        nextButton.interactable = state;
        backButton.interactable = state;
    }

    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        foreach (var panel in panels)
        {
            panel.DOKill();
        }
    }
}