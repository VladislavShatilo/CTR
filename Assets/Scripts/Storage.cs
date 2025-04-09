using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }
    public int  carCount = 13;
    public int money; 
    public int stars;
    public int score;
    public int[] levelsDones = new int[36];
    public int[] levelsStars = new int[36];
    public int activeSeason = 1;
    public int[] cars = new int[13];
    public int[] SelectedColor = new int[13];
    public int SelectedCar = 0;
    public int starsCount = 0;
    public int ButtonActivated = 0;
    public  int Season1Money = 200;
    public  int Season2Money = 500;
    public  int Season3Money = 2000;

    public float volume = 1;
    public int[] seasonCar = new int[3];
    public bool isHintShown = false; 
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
        if (levelsDones == null || levelsDones.Length != 36)
            levelsDones = new int[36];

        if (levelsStars == null || levelsStars.Length != 36)
            levelsStars = new int[36];

        if (cars == null || cars.Length != carCount)
            cars = new int[carCount];

        if (SelectedColor == null || SelectedColor.Length != carCount)
            SelectedColor = new int[carCount];

        if (seasonCar == null || seasonCar.Length != 3)
            seasonCar = new int[3];

        if (SelectedCar < 0 || SelectedCar >= carCount)
            SelectedCar = 0;
    }
    public void ResetSave()
    {

        levelsDones = new int[36];
        levelsStars = new int[36];
        cars = new int[carCount];
        SelectedColor = new int[carCount];
        seasonCar = new int[3];
        for (int i = 0; i < 36; i++)
        {
            levelsDones[i] = 0;
            levelsStars[i] = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            seasonCar[i] = 0;
        }

        for (int i = 0; i < carCount; i++)
        {
            cars[i] = 0;
            SelectedColor[i] = 0;
        }

        Save();
    }
}
