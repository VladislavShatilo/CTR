using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIAdvWindow : UIWindow
{
    [Header("UI")]
    [SerializeField] private Button closeButton;

    [SerializeField] private Button watchAdvButton;

    [Header("Settings")]
    [SerializeField] private float closeDelay = 0.35f;

    [Header("Referencies")]
    [SerializeField] private ArcadePlayerMovement arcadePlayerMovement;

    [SerializeField] private BuffManager buffManager;
    [SerializeField] private RoadGenerator roadGenerator;

    private void Start()
    {
        ComponentValidator.CheckAndLog(closeButton, nameof(closeButton), this);
        ComponentValidator.CheckAndLog(watchAdvButton, nameof(watchAdvButton), this);
        ComponentValidator.CheckAndLog(arcadePlayerMovement, nameof(arcadePlayerMovement), this);
        ComponentValidator.CheckAndLog(buffManager, nameof(buffManager), this);
        ComponentValidator.CheckAndLog(roadGenerator, nameof(roadGenerator), this);

        closeButton.onClick.AddListener(OnClose);
        watchAdvButton.onClick.AddListener(OnRewardClick);
    }

    private void OnEnable()
    {
        YG2.onRewardAdv += OnReward;
    }

    private void OnDestroy()
    {
        YG2.onRewardAdv -= OnReward;
        closeButton.onClick.RemoveListener(OnClose);
        watchAdvButton.onClick.RemoveListener(OnRewardClick);
    }

    private IEnumerator OnCloseCor()
    {
        CloseArcadeWindow();
        yield return new WaitForSeconds(closeDelay);

        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);

        var windowManager = UIArcadeManager.Instance.Windows;
        windowManager.ShowWindow<UIArcadeFinalWindow>();
        windowManager.GetWindow<UIArcadeFinalWindow>().CountScore();
    }

    private void OnClose()
    {
        StartCoroutine(OnCloseCor());
    }

    private void OnRewardClick()
    {
        YG2.RewardedAdvShow("Arcade");
    }

    private IEnumerator OnRewardCoroutine()
    {
        CloseArcadeWindow();
        yield return new WaitForSeconds(closeDelay);
        arcadePlayerMovement.RestartCar();
        buffManager.ActivateBuff(BuffType.Immortality);
        roadGenerator.Continue();
    }

    public void OnReward(string id)
    {
        if (id == "Arcade")
        {
            StartCoroutine(OnRewardCoroutine());
           
        }
    }
}