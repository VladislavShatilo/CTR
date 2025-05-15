using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    [SerializeField] private Sprite star, blackStar;
    [SerializeField] private int starsCount = 0;
    [SerializeField] private Text[] starsToUnlock;
    [SerializeField] private GameObject[] lockLevel;
    [SerializeField] private Button[] levelsG;
    [SerializeField] private Text[] starsToUnlockG;
    [SerializeField] private GameObject[] lockLevelG;
    private  int [] starsToUnlockNum = new int[2] {25,52};


    void Start()
    {
        for (int i = 3; i <= levels.Length + 2; i++)
        {
            if (Storage.Instance.levelsStars[i - 3] == 1)
            {
                levels[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levels[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = blackStar;
                levels[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = blackStar;
                levelsG[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = blackStar;
                levelsG[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = blackStar;
                starsCount += 1;

            }
            else if (Storage.Instance.levelsStars[i - 3] == 2)
            {
                levels[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levels[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = star;
                levels[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = blackStar;
                levelsG[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = blackStar;
                starsCount += 2;
            }
            else if (Storage.Instance.levelsStars[i - 3] == 3)
            {
                levels[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levels[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = star;
                levels[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(0).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(1).GetComponent<Image>().sprite = star;
                levelsG[i - 3].transform.GetChild(2).GetComponent<Image>().sprite = star;
                starsCount += 3;
            }
            else
            {
                levels[i - 3].transform.GetChild(0).gameObject.SetActive(false);
                levels[i - 3].transform.GetChild(1).gameObject.SetActive(false);
                levels[i - 3].transform.GetChild(2).gameObject.SetActive(false);
                levelsG[i - 3].transform.GetChild(0).gameObject.SetActive(false);
                levelsG[i - 3].transform.GetChild(1).gameObject.SetActive(false);
                levelsG[i - 3].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        Storage.Instance.totalStars = starsCount;
        for (int i = 0; i < starsToUnlock.Length; i++)
        {
            starsToUnlock[i].text = starsCount.ToString() + "/" + starsToUnlockNum[i].ToString();
            starsToUnlockG[i].text = starsCount.ToString() + "/" + starsToUnlockNum[i].ToString();

        }
        for (int i = 0; i < lockLevel.Length; i++)
        {
            if (starsCount >= starsToUnlockNum[i])
            {
                lockLevel[i].SetActive(false);
                lockLevelG[i].SetActive(false);
            }
            else
            {
                lockLevel[i].SetActive(true);
                lockLevelG[i].SetActive(true);
            }
        }
        Storage.Instance.Save();


    }


}
