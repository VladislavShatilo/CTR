using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Adv : MonoBehaviour
{
    private string rewardIDArcade = "Arcade";
    private string rewardIDShop = "Shop";

    private void OnEnable()
    {
        YG2.onRewardAdv += OnReward;    
       
    }

    private void OnDisable()
    {
        YG2.onRewardAdv -= OnReward;
    }

    
    public void OnReward(string id)
    {
        if(id == rewardIDArcade)
        {
            //FindObjectOfType<WindowAnimation>().CloseAdvWindow();
          
            FindObjectOfType<ArcadePlayerMovement>().RestartCar();
            FindAnyObjectByType<BuffManager>().SetImmortality();
            FindObjectOfType<RoadGenerator>().Continue();
        }
        else if(id == rewardIDShop)
        {
            CarShop carShop = FindObjectOfType<CarShop>();
            Storage.Instance.coins  += carShop.CalculateRewardCoins();
            carShop.UpdateCoinText();
            Storage.Instance.Save();
        }
    }
}
