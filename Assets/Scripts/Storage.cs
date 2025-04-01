using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }
    public string nameActiveScene = "Arcade";
    public const int  carCount = 13;
    public int money; 
    public int stars;
    public int score;
    public int[] levelsDones = new int[36];
    public int[] levelsStars = new int[36];
    public int activeSeason = 1;
    public int[] cars = new int[carCount];
    public int[] SelectedColor = new int[carCount];
    public int SelectedCar = 0;
    public int starsCount = 0;
    public int ButtonActivated = 0;
    public float volume = 1;
    public int[] seasonCar = new int[3];
    private const string SaveKey = "game_data";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            JsonUtility.FromJsonOverwrite(json, Instance);
        }
        else
        {
            ResetSave();
        }
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
