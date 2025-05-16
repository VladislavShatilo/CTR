using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[System.Serializable]
public class CarData
{
    public GameObject carObject;
    public string price;
    public bool isSeasonCar;
    public int seasonNumber;
    public float multiplier;
    public float carSize;
}

public class CarShop : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private CarData[] cars;

    [SerializeField] private Material[] carMaterials;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI carMultiplierText;
    [SerializeField] private TextMeshProUGUI noCoinsText;
    [SerializeField] private TextMeshProUGUI noMoneyRewardText;
    [SerializeField] private Image rewardImage;

    [Header("Buttons")]
    [SerializeField] private Button buyButton;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button colorButton;
    [SerializeField] private Button rewardButton;
    [SerializeField] private Button previousCarButton;
    [SerializeField] private Button nextCarButton;
    [SerializeField] private Button colorBtton;

    [Header("Windows")]
    [SerializeField] private GameObject block;

    [SerializeField] private GameObject noMoneyWindow;
    [SerializeField] private GameObject noSeasonWindow;

    [Header("Color Selection")]
    [SerializeField] private Toggle[] colorToggles;

    private Renderer[] carRenderers;
    private int selectedCarIndex;

    private void Awake()
    {
        InitializeComponents();
        SetupEventListeners();
        LoadSelectedCar();
        UpdateUI();
        UpdateCarDisplay();
    }
    private void OnEnable()
    {
        YG2.onRewardAdv += OnReward;
    }

    private void OnDisable()
    {
        YG2.onRewardAdv -= OnReward;

    }
    private void InitializeComponents()
    {
        carRenderers = new Renderer[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i].carObject != null)
            {
                carRenderers[i] = cars[i].carObject.GetComponent<Renderer>();
            }
        }

        Storage.Instance.cars[0] = 1;
    }

    private void SetupEventListeners()
    {
        buyButton.onClick.AddListener(BuyCar);
        selectButton.onClick.AddListener(SelectCar);
        unlockButton.onClick.AddListener(UnlockCar);
        rewardButton.onClick.AddListener(OpenReward);
        previousCarButton.onClick.AddListener(PreviousCar);
        nextCarButton.onClick.AddListener(NextCar);
        colorButton.onClick.AddListener(UpdateColorToggles);
        for (int i = 0; i < colorToggles.Length; i++)
        {
            int index = i;
            colorToggles[i].onValueChanged.AddListener(isOn => OnColorToggleChanged(isOn, index));
        }
    }

    private void LoadSelectedCar()
    {
        selectedCarIndex = Storage.Instance.selectedCar;
        SetCarColor(Storage.Instance.SelectedColor[selectedCarIndex]);
    }

    public void UpdateUI()
    {
        //UpdateCarDisplay();
        UpdateButtonStates();
        UpdateTexts();
    }

    private void UpdateCarDisplay()
    {
        foreach (var car in cars)
        {
            if (car.carObject != null)
            {
                car.carObject.SetActive(false);
            }
        }

        if (cars[selectedCarIndex].carObject != null)
        {
            cars[selectedCarIndex].carObject.SetActive(true);
        }
    }

    private void UpdateButtonStates()
    {
        bool isCarOwned = Storage.Instance.cars[selectedCarIndex] == 1;
        bool isSeasonCar = cars[selectedCarIndex].isSeasonCar;

        buyButton.gameObject.SetActive(!isCarOwned && !isSeasonCar);
        unlockButton.gameObject.SetActive(!isCarOwned && isSeasonCar);
        colorButton.gameObject.SetActive(isCarOwned);
        selectButton.gameObject.SetActive(isCarOwned);
        block.SetActive(!isCarOwned);
    }

    private void UpdateTexts()
    {
        coinsText.text = Storage.Instance.coins.ToString("N0");
        carMultiplierText.text = $"x {Storage.Instance.carMultiplier[selectedCarIndex]}";

        if (cars[selectedCarIndex].isSeasonCar)
        {
            if (YG2.envir.language == "ru")
            {
                priceText.text = $"Сезон {cars[selectedCarIndex].seasonNumber}";
            }
            else
            {
                priceText.text = $"Season {cars[selectedCarIndex].seasonNumber}";
            }
        }
        else
        {
            priceText.text = int.Parse(cars[selectedCarIndex].price).ToString("N0");
        }
    }

    private void UpdateColorToggles()
    {
        for (int i = 0; i < colorToggles.Length; i++)
        {
            colorToggles[i].isOn = (Storage.Instance.SelectedColor[selectedCarIndex] == i);
        }
    }

    private void OnColorToggleChanged(bool isOn, int colorIndex)
    {
        if (isOn)
        {
            SetCarColor(colorIndex);
            Storage.Instance.SelectedColor[selectedCarIndex] = colorIndex;
            Storage.Instance.Save();
        }
    }

    private void SetCarColor(int colorIndex)
    {
        if (carRenderers[selectedCarIndex] == null) return;

        Material[] materials = carRenderers[selectedCarIndex].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.StartsWith("!"))
            {
                materials[i] = carMaterials[colorIndex];
            }
        }
        carRenderers[selectedCarIndex].materials = materials;
        Storage.Instance.Save();

    }

    public void BuyCar()
    {
        int price = int.Parse(cars[selectedCarIndex].price);

        if (Storage.Instance.coins >= price)
        {
            Storage.Instance.coins -= price;
            Storage.Instance.cars[selectedCarIndex] = 1;
            Storage.Instance.Save();
            UpdateUI();
        }
        else
        {
            ShowNoMoneyWindow(price);
        }
    }

    private void ShowNoMoneyWindow(int price)
    {
        int moneyNeeded = price - Storage.Instance.coins;
        int rewardAmount = CalculateRewardCoins();
        if (YG2.envir.language == "ru")
        {
            noCoinsText.text = ("Не хватает монет\n{количество} нужно").Replace("{количество}", moneyNeeded.ToString("N0"));
            noMoneyRewardText.text = ("Смотреть зрекламу за {количество}").Replace("{количество}", rewardAmount.ToString());
        }
        else
        {
            noCoinsText.text = ("Not enough coins\n{amount} needed").Replace("{amount}", moneyNeeded.ToString("N0"));
            noMoneyRewardText.text = ("Watch ad for {amount}").Replace("{amount}", rewardAmount.ToString());
        }

        rewardImage.gameObject.SetActive(true);
        rewardButton.gameObject.SetActive(true);
        noMoneyWindow.SetActive(true);
    }

    public void UnlockCar()
    {
        CalculateSeasonUnlocks();
        int seasonIndex = cars[selectedCarIndex].seasonNumber - 1;

        if (Storage.Instance.seasonCarUnlocked[seasonIndex] == 1)
        {
            Storage.Instance.cars[selectedCarIndex] = 1;
            Storage.Instance.Save();
            UpdateUI();
        }
        else
        {
            noSeasonWindow.SetActive(true);
        }
    }

    public void SelectCar()
    {
        Storage.Instance.selectedCar = selectedCarIndex;
        Storage.Instance.Save();
    }

    public void PreviousCar()
    {
        int newIndex = (selectedCarIndex - 1 + cars.Length) % cars.Length;
        SwitchCarWithAnimation(newIndex);
       
    }

    public void NextCar()
    {
        int newIndex = (selectedCarIndex + 1) % cars.Length;

        SwitchCarWithAnimation(newIndex);
       
    }

    private void SwitchCarWithAnimation(int newIndex)
    {
        if (selectedCarIndex == newIndex) return;

        SetButtonsInteractable(false);

        cars[selectedCarIndex].carObject.transform.DOScale(0, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                cars[selectedCarIndex].carObject.SetActive(false);
                selectedCarIndex = newIndex;
                SetCarColor(Storage.Instance.SelectedColor[selectedCarIndex]);
                cars[selectedCarIndex].carObject.SetActive(true);
                cars[selectedCarIndex].carObject.transform.localScale = Vector3.zero;
                cars[selectedCarIndex].carObject.transform.DOScale(cars[selectedCarIndex].carSize, 0.3f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(OnAnimationComplete);
            });
    }
    private void OnAnimationComplete()
    {
        SetButtonsInteractable(true);
       
        UpdateUI();
    }
    private void SetButtonsInteractable(bool state)
    {
        buyButton.interactable = state;
        unlockButton.interactable = state;
        selectButton.interactable = state;
        colorButton.interactable = state;
        previousCarButton.interactable = state;
        nextCarButton.interactable = state;
    }

    private void CalculateSeasonUnlocks()
    {
        bool[] seasonCompleted = new bool[3] { true, true, true };

        for (int season = 0; season < 3; season++)
        {
            int startLevel = season * 12 + 1;
            int endLevel = (season + 1) * 12;

            for (int level = startLevel; level <= endLevel; level++)
            {
                if (Storage.Instance.levelsCompleted[level - 1] != 1)
                {
                    seasonCompleted[season] = false;
                    break;
                }
            }

            if (seasonCompleted[season])
            {
                Storage.Instance.seasonCarUnlocked[season] = 1;
            }
        }

        Storage.Instance.Save();
    }

    private int CalculateRewardCoins()
    {
        for (int i = Storage.carCount - 1; i >= 0; i--)
        {
            if (Storage.Instance.cars[i] == 1)
            {
                switch (i + 1)
                {
                    case 1: case 2: return 250;
                    case 3: case 4: case 5: case 6: return 1000;
                    case 7: return 2000;
                    case 8: case 9: return 4000;
                    case 10: case 11: return 15000;
                }
            }
        }
        return 100;
    }

    public void OpenReward()
    {
        YG2.RewardedAdvShow("Shop");
    }
    public void OnReward(string id)
    {
        if (id == "Shop")
        {
            Storage.Instance.coins += CalculateRewardCoins();
            Storage.Instance.Save();
            coinsText.text = Storage.Instance.coins.ToString("N0");

        }
    }
    private void OnDestroy()
    {
        // Clean up event listeners
        buyButton.onClick.RemoveAllListeners();
        selectButton.onClick.RemoveAllListeners();
        unlockButton.onClick.RemoveAllListeners();
        rewardButton.onClick.RemoveAllListeners();
        previousCarButton.onClick.RemoveAllListeners();
        nextCarButton.onClick.RemoveAllListeners();
        colorButton.onClick.RemoveAllListeners();

        foreach (var toggle in colorToggles)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}