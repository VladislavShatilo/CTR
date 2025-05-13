using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BlockRewardArcade : MonoBehaviour
{
    public string rewardID;
    public int timerDurationLock = 60;

    public bool timerComplete
    {
        get
        {
            if (!timersList.ContainsKey(rewardID))
                return true;
            else if (Time.realtimeSinceStartup >= timersList[rewardID] + timerDurationLock)
                return true;
            else
                return false;
        }
    }

    private static Dictionary<string, float> timersList = new Dictionary<string, float>();
    private Coroutine coroutine;

    private void OnEnable() => YG2.onRewardAdv += SetTimer;
    private void OnDisable() => YG2.onRewardAdv -= SetTimer;

    private void Start()
    {
        if (rewardID == "Arcade")
        {
            Storage.Instance.canShowArcadeRewardTime = true;
        }
        else if(rewardID == "Shop")
        {
            Storage.Instance.canShowShopRewardTime = true;

        }

        if (timersList.ContainsKey(rewardID))
        {

            if (timerComplete)
                timersList.Remove(rewardID);
            else
                coroutine = StartCoroutine(ShowTimer());
        }
    }

    private void SetTimer(string id)
    {
        if (id != rewardID)
            return;

        if (timersList.ContainsKey(id))
        {
            timersList[id] = Time.realtimeSinceStartup;
        }
        else
        {
            timersList.Add(id, Time.realtimeSinceStartup);
            coroutine = StartCoroutine(ShowTimer());
        }
    }

    IEnumerator ShowTimer()
    {


        while (!timerComplete)
        {
            string time;
            float timeInSeconds = timersList[rewardID] + timerDurationLock - Time.realtimeSinceStartup;
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

            if (minutes <= 0)
            {
                time = string.Format(seconds.ToString());
            }
            else
            {
                string str = string.Format("{0:00}:{1:00}", minutes, seconds);
                if (str[0].ToString() == "0")
                    str = str.Substring(1);
                time = str;
            }

            if (time == "0")
                break;

            if (rewardID == "Arcade")
            {
                Storage.Instance.canShowArcadeRewardTime = false;
            }
            else if (rewardID == "Shop")
            {
                Storage.Instance.canShowShopRewardTime = false;

            }
            

            yield return new WaitForSecondsRealtime(1);
        }
        if (rewardID == "Arcade")
        {
            Storage.Instance.canShowArcadeRewardTime = true;
        }
        else if (rewardID == "Shop")
        {
            Storage.Instance.canShowShopRewardTime = true;

        }
       

        timersList.Remove(rewardID);
    }
}
