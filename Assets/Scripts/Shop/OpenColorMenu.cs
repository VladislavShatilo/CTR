using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorMenuManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform[] panels;

    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private CarShop carShop;

    [Header("Animation Settings")]
    [SerializeField] private float transitionDuration = 0.5f;

    [SerializeField] private Ease transitionEase = Ease.InOutCubic;
    [SerializeField] private float buttonsDisableDuration = 0.25f;

    private int _currentIndex = 0;
    private Vector2 _centerPosition = Vector2.zero;
    private Vector2 _leftOffscreen;
    private Vector2 _rightOffscreen;
    private bool _isTransitioning;

    private void Start()
    {
        ValidateReferences();
        CalculateOffscreenPositions();
        InitializeButtons();
        ResetPanelsPosition();
    }

    private void ValidateReferences()
    {
        ComponentValidator.CheckAndLog(panels, nameof(panels), this);
        ComponentValidator.CheckAndLog(openButton, nameof(openButton), this);
        ComponentValidator.CheckAndLog(closeButton, nameof(closeButton), this);
        ComponentValidator.CheckAndLog(carShop, nameof(carShop), this);
    }

    private void CalculateOffscreenPositions()
    {
        float screenWidth = Screen.width * 1.5f;
        _leftOffscreen = new Vector2(-screenWidth, 0);
        _rightOffscreen = new Vector2(screenWidth, 0);

        if (panels.Length > 0 && panels[0] != null)
        {
            float panelWidth = panels[0].rect.width;
            if (panelWidth > Screen.width)
            {
                _leftOffscreen = new Vector2(-panelWidth * 1.5f, 0);
                _rightOffscreen = new Vector2(panelWidth * 1.5f, 0);
            }
        }
    }

    private void InitializeButtons()
    {
        openButton.onClick.AddListener(OpenMenu);
        closeButton.onClick.AddListener(CloseMenu);
    }

    private void ResetPanelsPosition()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] != null)
                panels[i].anchoredPosition = i == _currentIndex ? _centerPosition : _rightOffscreen;
        }
    }

    public void OpenMenu() => StartCoroutine(TransitionCoroutine(1));

    public void CloseMenu() => StartCoroutine(TransitionCoroutine(-1));

    private IEnumerator TransitionCoroutine(int direction)
    {
        if (_isTransitioning || panels.Length == 0) yield break;

        _isTransitioning = true;
        SetButtonsInteractable(false);

        int newIndex = (_currentIndex + direction + panels.Length) % panels.Length;

        if (panels[_currentIndex] != null)
        {
            panels[_currentIndex].DOAnchorPos(
                direction > 0 ? _leftOffscreen : _rightOffscreen,
                transitionDuration
            ).SetEase(transitionEase);
        }

        if (panels[newIndex] != null)
        {
            panels[newIndex].anchoredPosition = direction > 0 ? _rightOffscreen : _leftOffscreen;
            panels[newIndex].DOAnchorPos(_centerPosition, transitionDuration).SetEase(transitionEase);
        }

        carShop.UpdateUI();

        yield return new WaitForSeconds(buttonsDisableDuration);

        _currentIndex = newIndex;
        SetButtonsInteractable(true);
        _isTransitioning = false;
    }

    private void SetButtonsInteractable(bool state)
    {
        openButton.interactable = state;
        closeButton.interactable = state;
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        foreach (var panel in panels)
        {
            if (panel != null)
                panel.DOKill();
        }
    }
}