using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class ArcadeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoadGenerator roadGenerator;

    [SerializeField] private GameObject cityBackgroundGO;
    [SerializeField] private GameObject playerGO;
    public static ArcadeManager Instance { get; private set; }

    private CameraMovementArcade cameraMovement;
    private List<IArcadeStateListener> pauseListeners = new();
    private ArcadeCameraController cameraController;

    private void Awake()
    {
        
        YG2.StartInit();

        ComponentValidator.CheckAndLog(roadGenerator, nameof(roadGenerator), this);
        ComponentValidator.CheckAndLog(cityBackgroundGO, nameof(cityBackgroundGO), this);
        ComponentValidator.CheckAndLog(playerGO, nameof(playerGO), this);
        ComponentValidator.CheckAndLog(FindObjectsOfType<MonoBehaviour>().OfType<IArcadeStateListener>(),
            nameof(gameObject), this);

        pauseListeners = FindObjectsOfType<MonoBehaviour>().OfType<IArcadeStateListener>().ToList();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void SaveCoins()
    {
        if (Storage.Instance == null || UIArcadeManager.Instance.ArcadeHUD.ScoreText == null)
        {
            return;
        }
        if (int.TryParse(UIArcadeManager.Instance.ArcadeHUD.ScoreText.text,
          NumberStyles.AllowThousands,
          CultureInfo.InvariantCulture,
          out int currentScore))
        {
            Storage.Instance.coins += Storage.Instance.coinsInLevel;

            if (Storage.Instance.highScore < currentScore)
            {
                Storage.Instance.highScore = currentScore;
            }

            Storage.Instance.Save();
        }
    }

    private void Start()
    {
        Storage.Instance.coins += 10000000;
        for(int i =0; i < 36; i++)
        {
            Storage.Instance.levelsCompleted[i] = 1;
            Storage.Instance.levelsStars[i] = 3;
        }
        YG2.GameplayStart();
        if (!Camera.main.TryGetComponent(out cameraController))
        {
            Debug.LogError("CameraContoller is missing!");
            enabled = false;
        }
        if (!Camera.main.TryGetComponent(out cameraMovement))
        {
            Debug.LogError("CameraMovement is missing!");
            enabled = false;
        }
    }

    public void PauseGame()
    {
        foreach (var listener in pauseListeners)
        {
            listener.OnArcadePaused();
        }
    }

    public void ContinueGame()
    {
        foreach (var listener in pauseListeners)
        {
            listener.OnArcadeContinued();
        }
    }

    public void RestartGame()
    {
        Storage.Instance.coinsInLevel = 0;

        UIArcadeManager.Instance.ArcadeHUD.CoinsText.text = "0";
        foreach (var listener in pauseListeners)
        {
            listener.OnArcadeRestart();
        }
    }

    public void StartArcadeFromMenu()
    {
        StartCoroutine(StartArcadeFromMenuCor());
    }

    private IEnumerator StartArcadeFromMenuCor()
    {
        YG2.InterstitialAdvShow();
        yield return new WaitForSeconds(0.1f);

        cameraController.PlayIntroAnimation(playerGO.transform);

        RestartGame();

        UIArcadeManager.Instance.ArcadeHUD.SetActiveHUD(true);

        cameraMovement.enabled = false;
        yield return new WaitForSeconds(0.15f);
        cityBackgroundGO.SetActive(false);
    }

    public void ModifyRoadSpeed(float speed)
    {
        roadGenerator.ModifySpeed(speed);
    }

    public void StopRoadMovement()
    {
        roadGenerator.StopMovement();
    }
}