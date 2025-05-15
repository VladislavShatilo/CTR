using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }

    [Header("Game Settings")]
    public const int carCount = 13;
    public const int levelCount = 36;
    public const int seasonCount = 3;

    [Header("Economy Settings")]
    public float[] carMultiplier = new float[carCount]
    {
        1f, 1.3f, 1.6f, 2.1f, 2.7f, 3.5f,
        4.5f, 5.7f, 7.3f, 9.4f, 12f, 16f, 25f
    };

    [Header("Player Progress")]
    public int coins;
    public int coinsInLevel;
    public int totalStars;
    public int highScore;
    public int selectedCar;
    public float volume = 1;

    [Header("Level Progress")]
    public int[] levelsCompleted = new int[levelCount];
    public int[] levelsStars = new int[levelCount];

    [Header("Season Progress")]
    public int activeSeason = 1;
    public int[] seasonCarUnlocked = new int [seasonCount];


    [Header("Cars")]
    public int[] cars = new int[carCount];
    public int[] SelectedColor = new int[carCount];

    
    private const string SaveKey = "game_data";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
        Init();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Instance);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
       
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);

            JsonUtility.FromJsonOverwrite(json, Instance);
            Init();
            Save();
        }
        else
        {

            ResetSave();
        }
    }
    private void Init()
    {
        if (carMultiplier == null || carMultiplier.Length != carCount)
        {
            carMultiplier = new float[carCount];
        }
        if (levelsCompleted == null || levelsCompleted.Length != levelCount)
            levelsCompleted = new int[levelCount];

        if (levelsStars == null || levelsStars.Length != levelCount)
            levelsStars = new int[levelCount];

        if (cars == null || cars.Length != carCount)
            cars = new int[carCount];

        if (SelectedColor == null || SelectedColor.Length != carCount)
            SelectedColor = new int[carCount];

        if (seasonCarUnlocked == null || seasonCarUnlocked.Length != seasonCount)
            seasonCarUnlocked = new int[seasonCount];

        if (selectedCar < 0 || selectedCar >= carCount)
            selectedCar = 0;
    }
    public void ResetSave()
    {

        levelsCompleted = new int[levelCount];
        levelsStars = new int[levelCount];
        carMultiplier = new float[carCount] { 1,1.3f,1.6f,2.1f,2.7f,3.5f,4.5f,5.7f,7.3f,9.4f,12,16,25 };
        cars = new int[carCount];
        SelectedColor = new int[carCount];
        seasonCarUnlocked = new int[seasonCount];
        for (int i = 0; i < levelCount; i++)
        {
            levelsCompleted[i] = 0;
            levelsStars[i] = 0;
        }

        for (int i = 0; i < seasonCount; i++)
        {
            seasonCarUnlocked[i] = 0;
        }

        for (int i = 0; i < carCount; i++)
        {
            cars[i] = 0;
            SelectedColor[i] = 0;
        }

        Save();
    }
}
