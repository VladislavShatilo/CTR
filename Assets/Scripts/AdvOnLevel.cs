using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class AdvOnLevel : MonoBehaviour
{
    public Button ad1;
   
    // Start is called before the first frame update
    void Start()
    {
       // ad1.onClick.AddListener(delegate { OpenReward(2); });
        
    }

    private void Rewarded(int id)
    {
        if (id == 2)
        {
            AddMoney(300);
        }
    }
    void OpenReward(int id)
    {
        FindObjectOfType<AudioSource>().Stop();
        //YandexGame.RewVideoShow(id);
        FindObjectOfType<AudioSource>().Play();
    }

    public void AddMoney(int value)
    {
        int coins = Storage.Instance.money;
            //PlayerPrefs.GetInt("AllMoney");
        coins += 400;
       // PlayerPrefs.SetInt("AllMoney", coins);
        Storage.Instance.money = coins;
       // money.text =( 400 + int.Parse(money.text)).ToString();
        Storage.Instance.Save();

    }

  
}
