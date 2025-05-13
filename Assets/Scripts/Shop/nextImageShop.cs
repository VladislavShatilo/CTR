using System;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarShop : MonoBehaviour
{
    public GameObject[] cars;
    public Button buyButton;
    public Button selectButton;
    public Button unlockButton;
    public Button colorButton;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI moneyText;
    public int selectedCarIndex = 0;
    public string[] carPrices;

    public Toggle colorToggle;
    
    public Toggle[] colorToggles;
    private Renderer[] carRenderer;
    public Material[] carMaterials;
    [SerializeField] private Button rewardButton;
    [SerializeField] GameObject block;
    [SerializeField] GameObject NoMoneyWindow;
    [SerializeField] GameObject NoSeasonWindow;
    [SerializeField] TextMeshProUGUI noMoneyTxt;
    [SerializeField] TextMeshProUGUI noMoneyRewardTxt;
    [SerializeField] TextMeshProUGUI carMultiplierText;
    [SerializeField] private Image rewardImage;
    

    void Awake()
    {
        selectedCarIndex = Storage.Instance.SelectedCar;
        carRenderer = new Renderer[13];
        for (int i = 0; i < carRenderer.Length; i++)
        {
            carRenderer[i] = cars[i].GetComponent<Renderer>();
        }
      
        Storage.Instance.cars[0] = 1;
        int selectedColorIndex = Storage.Instance.SelectedColor[selectedCarIndex];
        SetCarColor(selectedColorIndex);

        for (int i = 0; i < colorToggles.Length; i++)
        {
            int colorIndex = i;

            colorToggles[i].onValueChanged.AddListener(isOn => OnToggleValueChanged(isOn, colorIndex));
        }
        UpdateUI();
        rewardButton.onClick.AddListener(delegate { OpenReward(); });
        Storage.Instance.Save();

    }

    void OnToggleValueChanged(bool isOn, int colorIndex)
    {
        if (isOn)
        {
            SetCarColor(colorIndex);
            Storage.Instance.SelectedColor[selectedCarIndex] = colorIndex;
            Storage.Instance.Save();
        }
    }


    void SetCarColor(int index)
    {
        Material[] materials = carRenderer[selectedCarIndex].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.StartsWith("!"))
            {
                materials[i] = carMaterials[index];

            }
        }
        carRenderer[selectedCarIndex].materials = materials;
        Storage.Instance.SelectedColor[selectedCarIndex] = index ;
        Storage.Instance.Save();

    }
    void OpenReward()
    {
        YG2.RewardedAdvShow("Shop");
    }
    public void BuyCar()
    {
        if (Storage.Instance.coins >= int.Parse(carPrices[selectedCarIndex]))            
        {
            Storage.Instance.coins = Storage.Instance.coins - int.Parse(carPrices[selectedCarIndex]);
            block.SetActive(false);
            Storage.Instance.cars[selectedCarIndex] = 1;
            
            UpdateUI();
            Storage.Instance.Save();
        }
        else
        {
            if (Storage.Instance.canShowShopRewardTime)
            {
                rewardImage.gameObject.SetActive(true);
                rewardButton.gameObject.SetActive(true);
            }
            else
            {
                rewardImage.gameObject.SetActive(false);

                rewardButton.gameObject.SetActive(false);
            }
            int moneyNeeded = int.Parse(carPrices[selectedCarIndex]) - Storage.Instance.coins;
            if(YG2.envir.language == "ru")
            {
                noMoneyTxt.text = "НЕ ХВАТАЕТ ДЕНЕГ\n" + moneyNeeded.ToString("N0") + " НУЖНО";
                noMoneyRewardTxt.text = "Смотреть рекламу за " + CalculateRewardCoins().ToString();

            }
            else
            {
                noMoneyTxt.text = "NOT ENOUGH MONEY\n" + moneyNeeded.ToString("N0") + " NEEDED";
                noMoneyRewardTxt.text = "Watch ad for " + CalculateRewardCoins().ToString();

            }
           
            NoMoneyWindow.SetActive(true);
        }

    }

    public void UpdateCoinText()
    {
        moneyText.text = Storage.Instance.coins.ToString("N0");

    }

    public void UnlockCar()
    {
        calculateSeason();

        int numberOfSeason = int.Parse(priceText.text[priceText.text.Length - 1].ToString());        
        if ( Storage.Instance.seasonCar[numberOfSeason - 1] == 1)
        {
            block.SetActive(false);
            Storage.Instance.cars[selectedCarIndex] = 1;
            Storage.Instance.Save   ();
            UpdateUI();
        }
        else
        {
            NoSeasonWindow.SetActive(true);
        }

    }
    public void SelectCar()
    {
        Storage.Instance.SelectedCar = selectedCarIndex;
        Storage.Instance.Save();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < colorToggles.Length; i++)
        {
            for(int j = 0; j< carRenderer[selectedCarIndex].materials.Length; j++)
            {

                if (colorToggles[i].image.color.Equals(carRenderer[selectedCarIndex].materials[j].color))
                {
                    colorToggles[i].isOn = true;
                }
            }
          

        }

        carMultiplierText.text =  "x "+Storage.Instance.carMultiplier[selectedCarIndex].ToString();
        moneyText.text = Storage.Instance.coins.ToString("N0");
        if (!carPrices[selectedCarIndex].StartsWith("Season"))
        {
            priceText.text = int.Parse(carPrices[selectedCarIndex]).ToString("N0");
        }
        else
        {
            string price = carPrices[selectedCarIndex];
            int numberOfSeason =int.Parse(price[price.Length - 1].ToString()) ;
            if (YG2.envir.language == "ru")
            {
                priceText.text = "Сезон " + numberOfSeason;

            }
            else
            {
                priceText.text = "Season " + numberOfSeason;

            }
        }

        SetActiveCar(selectedCarIndex);
        if (!carPrices[selectedCarIndex].StartsWith("Season"))
        {
            if (Storage.Instance.cars[selectedCarIndex] ==1)
            {
                buyButton.gameObject.SetActive(false);
                colorButton.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(true);
                block.SetActive(false);
            }
            else
            {
                block.SetActive(true);
                buyButton.gameObject.SetActive(true);
                colorButton.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Storage.Instance.cars[selectedCarIndex] == 1)
            {
                unlockButton.gameObject.SetActive(false);
                buyButton.gameObject.SetActive(false);
                colorButton.gameObject.SetActive(true);
                selectButton.gameObject.SetActive(true);
                block.SetActive(false);
            }
            else
            {
                block.SetActive(true);
                unlockButton.gameObject.SetActive(true);
                colorButton.gameObject.SetActive(false);
                buyButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(false);
            }
        }
    }

    public void PreviousCar()
    {
        selectedCarIndex = (selectedCarIndex - 1 + cars.Length) % cars.Length;
        int selectedColorIndex = Storage.Instance.SelectedColor[selectedCarIndex];
        SetCarColor(selectedColorIndex);
        UpdateUI();
    }

    public void NextCar()
    {
        selectedCarIndex = (selectedCarIndex + 1) % cars.Length;
        int selectedColorIndex = Storage.Instance.SelectedColor[selectedCarIndex];
        SetCarColor(selectedColorIndex);
        UpdateUI();
    }
    void SetActiveCar(int index)
    {
        foreach (var car in cars)
        {
            car.SetActive(false);
        }

        cars[index].SetActive(true);
    }

    public int CalculateRewardCoins()
    {
        int amoutOfCoins = 100;
        for (int i = Storage.Instance.carCount - 1; i >= 0; i--)
        {
            if (Storage.Instance.cars[i] == 1)
            {
                //i+1 is not open
                switch (i + 1)
                {
                    case 1:
                        return 250;                    
                    case 2:
                        return 250;
                    case 3:
                        return 1000;
                    case 4:
                        return 1000;
                    case 5:
                        return 1000;
                    case 6:
                        return 1000;                      
                    case 7:
                        return 2000;                    
                    case 8:
                        return 4000;                 
                    case 9:
                        return 4000;
                    case 10:
                        return 15000;
                    case 11:
                        return 15000;
                    case 12:
                        return 0;
                }
            }
        }
        return amoutOfCoins;
    }
    private void calculateSeason()
    {
        bool season1 = true;
        bool season2 = true;
        bool season3 = true;
     
        for (int i = 1; i <= 12; i++)
        {
            if (Storage.Instance.levelsDones[i - 1] != 1)
            {
                 season1= false;
            }
        }
        for (int i = 13; i <= 24; i++)
        {
            if (Storage.Instance.levelsDones[i - 1] != 1)
            {
                season2 = false;
            }
        }
        for (int i = 25; i <= 36; i++)
        {
            if (Storage.Instance.levelsDones[i - 1] != 1)
            {
                season3 = false;

            }
        }
       
        if (season1)
        {
            Storage.Instance.seasonCar[0] = 1;
        }
        if (season2)
        {
            Storage.Instance.seasonCar[1] = 1;
        }
        if (season3)
        {
            Storage.Instance.seasonCar[2] = 1;
        }
        Storage.Instance.Save();

    }
}


