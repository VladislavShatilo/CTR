using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainManager : MonoBehaviour
{

    [SerializeField] public GameObject[] cars;
    [SerializeField] private Material[] carMaterials;

    private Text moneyInGame;
    private Renderer[] carRenderer;
    private int countOfCoinsInLevels;


    private void Awake()
    { 
        Application.targetFrameRate = 60;
        Camera.main.transform.SetParent(null);
        moneyInGame = UIArcadeController.Instance.MoneyInGameText;
        
    }
    void Start()
    {
       StartCor();

    }
    void StartCor()
    {
        
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

   
}
