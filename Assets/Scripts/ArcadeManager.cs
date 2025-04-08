using System.Collections;
using System.Collections.Generic;
    using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGO; 
    [SerializeField] private List<ArcadeBuffTimer> arcadeBuffTimers;
    [SerializeField] private GameObject roadGeneratorGO;
    [SerializeField] private GameObject cityBackgroundGO;

    private CameraMovementArcade cameraMovement;
    private Text countDownText;
    private ArcadePlayerMovement arcadePlayerMovement;
    private RoadGenerator roadGenerator;

    private void Start()
    {
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

        roadGenerator.Continue();
        arcadePlayerMovement.enabled = true;

        foreach (var arcadeBuffTimer in arcadeBuffTimers)
        {
            arcadeBuffTimer.ContinueTimer();
        }
    }
   
    public void PauseGame()
    {
        UIArcadeController.Instance.PauseWindowGO.SetActive(true);
        roadGenerator.Pause();
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
        playerGO.GetComponent<buffScripts>().Restart();
        arcadePlayerMovement.enabled = true;

        foreach(var arcadeBuffTimer in arcadeBuffTimers)
        {
            arcadeBuffTimer.StopTimer();
        }

        FindObjectOfType<PointsCounter>().currentScore = 0f;
        controller.MoneyInGameText.text = "0";
        FindObjectOfType<mainManager>().SetZeroOnCoinsInLevel();
    }

    public void StartArcadeFromMenu()
    {
        StartCoroutine(StartArcadeFromMenuCor());
    }

    private IEnumerator StartArcadeFromMenuCor()
    {
        roadGeneratorGO.SetActive(true);
        UIArcadeController.Instance.InGameCanvas.gameObject.SetActive(true);
        arcadePlayerMovement.enabled = true;
        arcadePlayerMovement.isGameIntro = true;

        cameraMovement.enabled = false;
        UIArcadeController.Instance.MenuCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        if (!Storage.Instance.isHintShown)
        {
            FindObjectOfType<Hint>().StartHint();
            Storage.Instance.isHintShown = true;
            Storage.Instance.Save();
        }
        cityBackgroundGO.SetActive(false);
    }
}
