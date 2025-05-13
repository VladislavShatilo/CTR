using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;
using TMPro.Examples;
using System;
using System.Linq;
public class ArcadeManager : MonoBehaviour
{

    public event Action OnArcadePause;
    public event Action OnArcadeContinue;
    public event Action OnArcadeRestart;

    [Header("References")]
    [SerializeField] private RoadGenerator roadGenerator;
    [SerializeField] private GameObject cityBackgroundGO;

    public static ArcadeManager Instance { get; private set; }

    private CameraMovementArcade cameraMovement;
    private List<IArcadePauseListener> pauseListeners = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        pauseListeners = FindObjectsOfType<MonoBehaviour>().OfType<IArcadePauseListener>().ToList();
    }
    private void Start()
    {
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
        UIManager.Instance.HUD.CoinsText.text = "0";
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
        RestartGame();
        UIManager.Instance.HUD.SetActiveHUD(true);
        cameraMovement.enabled = false;
        yield return new WaitForSeconds(0.15f);
        cityBackgroundGO.SetActive(false);
    }

    public void AddSpeed(float buff)
    {
        roadGenerator.AddSpeed(buff);
    }
    public void SetZeroSpeed()
    {
        roadGenerator.SetZeroSpeed();
    }
}
