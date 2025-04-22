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
        if(YG2.envir.language == "ru")
        {
            UIController.Instance.LevelText.text = "Уровень " + SceneManager.GetActiveScene().name;

        }
        else
        {
            UIController.Instance.LevelText.text = "Level " + SceneManager.GetActiveScene().name;

        }
        PlayerMove.Instance.enabled = false;
        UIController.Instance.UIMenuGameHide(false);
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
        UIController controller = UIController.Instance;
        controller.PlayButton.gameObject.SetActive(false);
        controller.TapToPlayText.gameObject.SetActive(false);
        PlayerMove.Instance.enabled = true;
        controller.MainLevelMenuButton.gameObject.SetActive(false);
        controller.UIMenuGameHide(true);

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
                Storage.Instance.money = Storage.Instance.money + Storage.Instance.Season1Money;
            }
            else if(level >= 13 && level <= 24)
            {
                Storage.Instance.money = Storage.Instance.money + Storage.Instance.Season2Money;

            }
            else
            {
                Storage.Instance.money = Storage.Instance.money + Storage.Instance.Season3Money;
            }

            Storage.Instance.Save();
        }
        else
        {
            UIController.Instance.LevelCoinsWinText.text = "0";
        }
    }
}
