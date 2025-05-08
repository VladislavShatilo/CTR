using System.Collections;
using System.Collections.Generic;
    using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;
using TMPro.Examples;
public class ArcadeManager : MonoBehaviour
{
    public static ArcadeManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject playerGO; 
    [SerializeField] private List<ArcadeBuffTimer> arcadeBuffTimers;
    [SerializeField] private GameObject roadGeneratorGO;
    [SerializeField] private GameObject cityBackgroundGO;
    [SerializeField] private GameObject hintGO;
    [SerializeField] private PointsCounter pointsCounter;

   

    private CameraMovementArcade cameraMovement;
    private TextMeshProUGUI countDownText;
    private ArcadePlayerMovement arcadePlayerMovement;
    private RoadGenerator roadGenerator;
    private UIArcadeController UIArcadeController;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        UIArcadeController = UIArcadeController.Instance;
        countDownText = UIArcadeController.Instance.CountDownInGameText;
        arcadePlayerMovement = playerGO.GetComponent<ArcadePlayerMovement>();
        roadGenerator = roadGeneratorGO.GetComponent<RoadGenerator>();
        cameraMovement = Camera.main.GetComponent<CameraMovementArcade>(); 
        if(countDownText == null || arcadePlayerMovement == null || roadGenerator == null 
            || cameraMovement == null || UIArcadeController==null)
        {
            Debug.LogError("ArcadeManager: Critical components are missing!");
            enabled = false;
        }
    }
    IEnumerator StartCountdown()
    {
        var wait = new WaitForSecondsRealtime(0.7f);
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return wait;
        }
        countDownText.gameObject.SetActive(false);

        roadGenerator.ContinueButton();
        pointsCounter.StartCounter();

        arcadePlayerMovement.enabled = true;

        SetAllTimersActive(true);
    }
   
    public void PauseGame()
    {
        UIArcadeController.PauseWindowGO.SetActive(true);
        roadGenerator.PauseButton();
        pointsCounter.StopCounter();

        arcadePlayerMovement.enabled = false;

        SetAllTimersActive(false);
    }

    public void ContinueGame()
    {
        UIArcadeController.PauseWindowGO.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    public void RestartGame()
    {

        UIArcadeController.HideAllGameWindows();
        UIArcadeController.AdvWindowBGImage.gameObject.SetActive(true);


        roadGenerator.Restart();
        arcadePlayerMovement.RestartCar();

        playerGO.SetActive(true);
        playerGO.GetComponent<BuffManager>().Restart();
        arcadePlayerMovement.enabled = true;

        SetAllTimersActive(false);
        pointsCounter.currentScore = 0f;
        UIArcadeController.MoneyInGameText.text = "0";
        MainManager.Instance.SetZeroOnCoinsInLevel();
    }
    public void ContinueRoadGen()
    {
        roadGenerator.Continue();


    }
    public void StartArcadeFromMenu()
    {
        StartCoroutine(StartArcadeFromMenuCor());
    }

    private IEnumerator StartArcadeFromMenuCor()
    {
        YG2.InterstitialAdvShow();
        yield return new WaitForSeconds(0.1f);

        roadGeneratorGO.SetActive(true);

        UIArcadeController.InGameCanvas.gameObject.SetActive(true);
        arcadePlayerMovement.enabled = true;
       

        cameraMovement.enabled = false;
        UIArcadeController.MenuCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        roadGenerator.Pause();
       
        if (!Storage.Instance.isHintShown)
        {
            hintGO.SetActive(true);
            Storage.Instance.isHintShown = true;
            Storage.Instance.Save();
        }
        else
        {
            roadGenerator.Continue();
        }
        cityBackgroundGO.SetActive(false);
    }
    private void SetAllTimersActive(bool isActive)
    {
        foreach (var timer in arcadeBuffTimers)
        {
            if (isActive) timer.ContinueTimer();
            else timer.PauseTimer();
        }
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
