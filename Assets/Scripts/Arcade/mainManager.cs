using System.Globalization;
using UnityEngine;
using YG;

public class MainManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Material[] carMaterials;

    [SerializeField] private CarProvider carProvider;

    [Header("Settings")]
    [SerializeField] private int targetFrameRate = 60;

    private GameObject[] cars;
    private Renderer[] carRenderers;
    public static MainManager Instance { get; private set; }

    private void Awake()
    {
        YG2.StartInit();
        Application.targetFrameRate = targetFrameRate;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        YG2.GameplayStart();

        InitializeCarSystem();
        UpdateUI();
    }

    private void InitializeCarSystem()
    {
        ComponentValidator.CheckAndLog(carProvider, nameof(carProvider), this);
        cars = carProvider.Cars;

        InitializeCarRenderers();
        ActivateSelectedCar();
        ApplySelectedColor();
    }

    private void InitializeCarRenderers()
    {
        carRenderers = new Renderer[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i] != null)
            {
                carRenderers[i] = cars[i].GetComponent<Renderer>();
            }
        }
    }

    private void ActivateSelectedCar()
    {
        foreach (var car in cars)
        {
            car.SetActive(false);
        }

        int selectedIndex = Mathf.Clamp(Storage.Instance.selectedCar, 0, cars.Length - 1);
        cars[selectedIndex].SetActive(true);
    }

    private void ApplySelectedColor()
    {
        int selectedCar = Storage.Instance.selectedCar;
        if (selectedCar < 0 || selectedCar >= carRenderers.Length || carRenderers[selectedCar] == null)
        {
            return;
        }

        var materials = carRenderers[selectedCar].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (IsBodyMaterial(materials[i]))
            {
                int colorIndex = Mathf.Clamp(Storage.Instance.SelectedColor[selectedCar], 0, carMaterials.Length - 1);
                materials[i] = carMaterials[colorIndex];
                break;
            }
        }
        carRenderers[selectedCar].materials = materials;
    }

    private bool IsBodyMaterial(Material mat)
    {
        string lowerName = mat.name.ToLower();
        return lowerName.Contains("Body");
    }

    private void UpdateUI()
    {
        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.MenuManager, nameof(UIArcadeManager.Instance.MenuManager), this);

        UIArcadeManager.Instance.MenuManager.UpdateStatesInfo();
    }

    public void SaveCoinsInLevel()
    {
        if (Storage.Instance == null || UIArcadeManager.Instance.ArcadeHUD.ScoreText == null)
        {
            return;
        }
        if (int.TryParse(UIArcadeManager.Instance.ArcadeHUD.ScoreText.text,
          NumberStyles.AllowThousands,
          CultureInfo.InvariantCulture,
          out int currentScore))
        {
            Storage.Instance.coins += Storage.Instance.coinsInLevel;

            if (Storage.Instance.highScore < currentScore)
            {
                Storage.Instance.highScore = currentScore;
            }

            Storage.Instance.Save();
        }
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