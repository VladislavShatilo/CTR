using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;


/// <summary>
/// Central game manager for arcade mode handling game state, events, and core systems
/// </summary>
public class ArcadeManager : MonoBehaviour
{

    public event Action OnArcadePause;
    public event Action OnArcadeContinue;
    public event Action OnArcadeRestart;

    [Header("References")]
    [SerializeField] private RoadGenerator roadGenerator;
    [SerializeField] private GameObject cityBackgroundGO;
    [SerializeField] private GameObject playerGO;
    public static ArcadeManager Instance { get; private set; }

    private CameraMovementArcade cameraMovement;
    private List<IArcadeStateListener> pauseListeners = new();
    private ArcadeCameraController cameraController;

    void Awake()
    {
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
    private void Start()
    {
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