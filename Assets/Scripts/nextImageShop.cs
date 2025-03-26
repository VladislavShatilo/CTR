using System;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarShop : MonoBehaviour
{
    public GameObject[] cars;
    public Button buyButton;
    public Button selectButton;
    public Button unlockButton;
    public Button colorButton;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI money;
    public int selectedCarIndex = 0;
    public string[] carPrices;

    public Toggle colorToggle;
    
    public Toggle[] colorToggles;
    private Renderer[] carRenderer;
    public Material[] carMaterials;

    [SerializeField] GameObject block;
    [SerializeField] GameObject NoMoneyWindow;
    [SerializeField] GameObject NoSeasonWindow;
    [SerializeField] TextMeshProUGUI noMoneyTxt;

    [SerializeField] float[] carMultiplier;
    [SerializeField] TextMeshProUGUI carMultiplierText;
    

    void Awake()
    {
        selectedCarIndex = Storage.Instance.SelectedCar;
        carRenderer = new Renderer[14];
        for (int i = 0; i < carRenderer.Length; i++)
        {
            carRenderer[i] = cars[i].GetComponent<Renderer>();
        }
       // YandexGame.FullscreenShow();
        //for(int i = 0; i < carRenderer.Length; i++)
        //{
        //    Storage.Instance.cars[i] = 1;

        //}
        Storage.Instance.cars[0] = 1;
        int selectedColorIndex = Storage.Instance.SelectedColor[selectedCarIndex];
        SetCarColor(selectedColorIndex);

        for (int i = 0; i < colorToggles.Length; i++)
        {
            int colorIndex = i;

            colorToggles[i].onValueChanged.AddListener(isOn => OnToggleValueChanged(isOn, colorIndex));
        }
        UpdateUI();
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
    
    public void BuyCar()
    {
        if (Storage.Instance.money >= int.Parse(carPrices[selectedCarIndex]))            
        {
            Storage.Instance.money = Storage.Instance.money - int.Parse(carPrices[selectedCarIndex]);
            block.SetActive(false);
            Storage.Instance.cars[selectedCarIndex] = 1;
            
            UpdateUI();
            Storage.Instance.Save();
        }
        else
        {
            int moneyNeeded = int.Parse(carPrices[selectedCarIndex]) - Storage.Instance.money;
            noMoneyTxt.text = moneyNeeded.ToString("N0");
            NoMoneyWindow.SetActive(true);
        }

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

        carMultiplierText.text =  carMultiplier[selectedCarIndex].ToString();
        money.text = Storage.Instance.money.ToString("N0");
        if (!carPrices[selectedCarIndex].StartsWith("Season"))
        {
            priceText.text = int.Parse(carPrices[selectedCarIndex]).ToString("N0");
        }
        else
        {
            string price = carPrices[selectedCarIndex];
            int numberOfSeason =int.Parse(price[price.Length - 1].ToString()) ;
            priceText.text = "Season " + numberOfSeason;
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


