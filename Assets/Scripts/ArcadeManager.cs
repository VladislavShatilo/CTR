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

    [SerializeField] private GameObject playerGO; 
    [SerializeField] private List<ArcadeBuffTimer> arcadeBuffTimers;
    [SerializeField] private GameObject roadGeneratorGO;
    [SerializeField] private GameObject cityBackgroundGO;
    [SerializeField] private GameObject hintGO;
    [Header("Camera")]
    [SerializeField] private ArcadeCameraController cameraController;
    private CameraMovementArcade cameraMovement;
    private TextMeshProUGUI countDownText;
    private ArcadePlayerMovement arcadePlayerMovement;
    private RoadGenerator roadGenerator;
    private PointsCounter pointsCounter;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        pointsCounter = FindObjectOfType<PointsCounter>();
          countDownText = UIArcadeController.Instance.CountDownInGameText;
        arcadePlayerMovement = playerGO.GetComponent<ArcadePlayerMovement>();
        roadGenerator = roadGeneratorGO.GetComponent<RoadGenerator>();
        cameraMovement = Camera.main.GetComponent<CameraMovementArcade>(); 
    }
    IEnumerator StartCountdown()
    {
        countDownText.text = "3";
        countDownText.gameObject.SetActive(true); 
        yield return new WaitForSecondsRealtime(0.7f);
        countDownText.text = "2";
        yield return new WaitForSecondsRealtime(0.7f);
        countDownText.text = "1";
        yield return new WaitForSecondsRealtime(0.7f);
        countDownText.gameObject.SetActive(false);

        roadGenerator.ContinueButton();
        pointsCounter.StartCounter();

        arcadePlayerMovement.enabled = true;

        foreach (var arcadeBuffTimer in arcadeBuffTimers)
        {
            arcadeBuffTimer.ContinueTimer();
        }
    }
   
    public void PauseGame()
    {
        UIArcadeController.Instance.PauseWindowGO.SetActive(true);
        roadGenerator.PauseButton();
        pointsCounter.StopCounter();

        arcadePlayerMovement.enabled = false;

        foreach(var arcadeBuffTimer in arcadeBuffTimers)
        {
            arcadeBuffTimer.PauseTimer();
        }
    }

    public void ContinueGame()
    {
        UIArcadeController.Instance.PauseWindowGO.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    public void RestartGame()
    {
        UIArcadeController controller = UIArcadeController.Instance;
        controller.PauseWindowGO.SetActive(false);
        controller.FinalWindowGO.SetActive(false);
        controller.AdvWindowGO.SetActive(false);
        controller.AdvWindowBGImage.gameObject.SetActive(true);


        roadGenerator.Restart();
        FindObjectOfType<ArcadePlayerMovement>().RestartCar();

        playerGO.SetActive(true);
        playerGO.GetComponent<BuffManager>().Restart();
        arcadePlayerMovement.enabled = true;

        foreach(var arcadeBuffTimer in arcadeBuffTimers)
        {
            arcadeBuffTimer.StopTimer();
        }

        FindObjectOfType<PointsCounter>().currentScore = 0f;
        controller.MoneyInGameText.text = "0";
        FindObjectOfType<MainManager>().SetZeroOnCoinsInLevel();
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

        UIArcadeController.Instance.InGameCanvas.gameObject.SetActive(true);
        arcadePlayerMovement.enabled = true;
        cameraController = Camera.main.GetComponent<ArcadeCameraController>();
        cameraController.PlayIntroAnimation(transform);

        cameraMovement.enabled = false;
        UIArcadeController.Instance.MenuCanvas.gameObject.SetActive(false);
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

    public void AddSpeed(float buff)
    {
        roadGenerator.AddSpeed(buff);
    }
    public void SetZeroSpeed()
    {
        roadGenerator.SetZeroSpeed();
    }
}
