using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelUI
    {
        public Button button;
        public Image[] stars;
    }

    [Header("Settings")]
    [SerializeField] private Sprite filledStar;

    [SerializeField] private Sprite emptyStar;

    [Header("Portrait UI")]
    [SerializeField] private LevelUI[] portraitLevels;

    [SerializeField] private TextMeshProUGUI[] starsUnlockTextPortrait;
    [SerializeField] private GameObject[] blockSeasontPortrait;

    [Header("Landscape UI")]
    [SerializeField] private LevelUI[] landscapeLevels;

    [SerializeField] private TextMeshProUGUI[] starsUnlockTextLandscape;
    [SerializeField] private GameObject[] blockSeasontLandscape;

    private int totalStars;
    private int[] starsToUnlockNum = new int[2] { 25, 52 };

    private void Awake()
    {
        ComponentValidator.CheckAndLog(filledStar, nameof(filledStar), this);
        ComponentValidator.CheckAndLog(emptyStar, nameof(emptyStar), this);
        ComponentValidator.CheckAndLog(portraitLevels, nameof(portraitLevels), this);
        ComponentValidator.CheckAndLog(starsUnlockTextPortrait, nameof(starsUnlockTextPortrait), this);
        ComponentValidator.CheckAndLog(blockSeasontPortrait, nameof(blockSeasontPortrait), this);
        ComponentValidator.CheckAndLog(landscapeLevels, nameof(landscapeLevels), this);
        ComponentValidator.CheckAndLog(starsUnlockTextLandscape, nameof(starsUnlockTextLandscape), this);
        ComponentValidator.CheckAndLog(blockSeasontLandscape, nameof(blockSeasontLandscape), this);
    }

    private void Start()
    {
        InitializeLevels();
        UpdateUnlcokTexts();
        UnlockSeason();
        Storage.Instance.Save();
    }

    private void InitializeLevels()
    {
        totalStars = 0;

        for (int i = 0; i < Storage.Instance.levelsStars.Length; i++)
        {
            int stars = Storage.Instance.levelsStars[i];
            totalStars += stars;

            UpdateLevelStars(portraitLevels[i], stars);
            UpdateLevelStars(landscapeLevels[i], stars);
        }

        Storage.Instance.totalStars = totalStars;
    }

    private void UpdateLevelStars(LevelUI level, int starsCount)
    {
        if (level == null) return;

        bool hasStars = starsCount > 0;

        for (int j = 0; j < level.stars.Length; j++)
        {
            if (level.stars[j] != null)
            {
                level.stars[j].gameObject.SetActive(hasStars);
                level.stars[j].sprite = j < starsCount ? filledStar : emptyStar;
            }
        }
    }

    private void UpdateUnlcokTexts()
    {
        for (int i = 0; i < starsUnlockTextLandscape.Length; i++)
        {
            starsUnlockTextLandscape[i].text = starsUnlockTextPortrait[i].text = totalStars.ToString() + "/" + starsToUnlockNum[i].ToString();
        }
    }

    private void UnlockSeason()
    {
        for (int i = 0; i < blockSeasontPortrait.Length; i++)
        {
            if (totalStars >= starsToUnlockNum[i])
            {
                blockSeasontPortrait[i].SetActive(false);
                blockSeasontLandscape[i].SetActive(false);
            }
            else
            {
                blockSeasontPortrait[i].SetActive(true);
                blockSeasontLandscape[i].SetActive(true);
            }
        }
    }
}