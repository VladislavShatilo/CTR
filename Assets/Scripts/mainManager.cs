using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using TMPro;

public class MainManager : MonoBehaviour
{

    [SerializeField] private Material[] carMaterials;
    private GameObject[] cars;
    private TextMeshProUGUI moneyInGame;
    private Renderer[] carRenderer;
    private int countOfCoinsInLevels;
    public static MainManager Instance { get; private set; }

    private void Awake()
    {
        YG2.StartInit();
        Application.targetFrameRate = 60;
        Camera.main.transform.SetParent(null);
        moneyInGame = UIArcadeController.Instance.MoneyInGameText;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }
    void Start()
    {
        YG2.GameplayStart();

        StartCor();

    }
    void StartCor()
    {
        cars = CarProvider.Instance.Cars;
        carRenderer = new Renderer[cars.Length];
        for (int i = 0; i < carRenderer.Length; i++)
        {
            carRenderer[i] = cars[i].GetComponent<Renderer>();
        }

        UIArcadeController.Instance.SetMenuStatistic();
        //YandexGame.FullscreenShow();

        foreach (var car in cars)
        {
            car.SetActive(false);
        }

        cars[Storage.Instance.SelectedCar].SetActive(true);
        Material[] materials = carRenderer[Storage.Instance.SelectedCar].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.Contains("!"))
            {
                materials[i] = carMaterials[Storage.Instance.SelectedColor[Storage.Instance.SelectedCar]];
            }
        }
        carRenderer[Storage.Instance.SelectedCar].materials = materials;
        //YandexGame.NewLeaderboardScores("Record", Storage.Instance.score);
    }
   

    public void AddOneCoinInLevel(int count)
    {
        countOfCoinsInLevels += count;
        moneyInGame.text = countOfCoinsInLevels.ToString();
    }

    public void SetZeroOnCoinsInLevel()
    {
        countOfCoinsInLevels = 0;
    }

    public void SaveCoinsInLevel()
    {

        Storage.Instance.money += countOfCoinsInLevels;
        if (Storage.Instance.score < int.Parse(UIArcadeController.Instance.ScoreInGameText.text, System.Globalization.NumberStyles.AllowThousands))
        {
            Storage.Instance.score = int.Parse(UIArcadeController.Instance.ScoreInGameText.text, System.Globalization.NumberStyles.AllowThousands);
        }

        Storage.Instance.Save();
    }

    public void ShowRewardArcade()
    {
        YG2.RewardedAdvShow("Arcade");
    }

    public void ShowMidgame()
    {
        YG2.InterstitialAdvShow();

    }

  
}
