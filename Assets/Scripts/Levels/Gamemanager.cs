using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Gamemanager : MonoBehaviour
{
    private Camera cachedCamera;
    public bool isPauseGlobal;
  
    private void Start()
    {
       
        PlayerMove.Instance.enabled = false;
        UILevelManager.Instance.LevelHUD.SetGameplayUI(false);
        cachedCamera = Camera.main;

    }
    public void PauseFunction(bool isPause)
    {
        if (isPause)
        {
            isPauseGlobal = true;
            PlayerMove.Instance.enabled = false;
        }
        else
        {
            isPauseGlobal = false;
            PlayerMove.Instance.enabled = true;

        }
    }
    public void StartGame()
    {
     
        PlayerMove.Instance.enabled = true;
        
    }

    public void StopCamera()
    {
        cachedCamera.GetComponent<CameraMovement>().enabled = false;
    }
    public void EarnMoneyForLevel()
    {
        int level = int.Parse(SceneManager.GetActiveScene().name);

        if (Storage.Instance.levelsDones[level - 1] != 1)
        {
            if(level >= 1 && level <= 12)
            {
                Storage.Instance.coins = Storage.Instance.coins + Storage.Instance.Season1Money;
            }
            else if(level >= 13 && level <= 24)
            {
                Storage.Instance.coins = Storage.Instance.coins + Storage.Instance.Season2Money;

            }
            else
            {
                Storage.Instance.coins = Storage.Instance.coins + Storage.Instance.Season3Money;
            }

            Storage.Instance.Save();
        }
        else
        {
            //UIController.Instance.LevelCoinsWinText.text = "0";
        }
    }
}
