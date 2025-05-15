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
    [Header("Dependencies")]

    [SerializeField] private Material[] carMaterials;
    [SerializeField] private CarProvider carProvider;
    [SerializeField] private UIArcadeManager uiArcadeManager;

    [Header("Settings")]
    [SerializeField] private int _targetFrameRate = 60;

    private GameObject[] cars;
    private Renderer[] carRenderers;
    public static MainManager Instance { get; private set; }

    private void Awake()
    {
        YG2.StartInit();
        Application.targetFrameRate = 60;
      
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
        carRenderers = new Renderer[cars.Length];
        for (int i = 0; i < carRenderers.Length; i++)
        {
            carRenderers[i] = cars[i].GetComponent<Renderer>();
        }
        UIArcadeManager.Instance.MenuManager.UpdateStatesInfo();
      

        foreach (var car in cars)
        {
            car.SetActive(false);
        }

        cars[Storage.Instance.SelectedCar].SetActive(true);
        Material[] materials = carRenderers[Storage.Instance.SelectedCar].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.Contains("!"))
            {
                materials[i] = carMaterials[Storage.Instance.SelectedColor[Storage.Instance.SelectedCar]];
            }
        }
        carRenderers[Storage.Instance.SelectedCar].materials = materials;
        //YandexGame.NewLeaderboardScores("Record", Storage.Instance.score);
    }
   


    public void SaveCoinsInLevel()
    {
        Storage.Instance.coins += Storage.Instance.coinsInLevel;
        if (Storage.Instance.score < int.Parse(UIArcadeManager.Instance.ArcadeHUD.ScoreText.text, System.Globalization.NumberStyles.AllowThousands))
        {
            Storage.Instance.score = int.Parse(UIArcadeManager.Instance.ArcadeHUD.ScoreText.text, System.Globalization.NumberStyles.AllowThousands);
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
