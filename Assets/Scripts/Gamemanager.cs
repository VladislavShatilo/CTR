using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    private Camera cachedCamera;

    private void Start()
    {
        UIController.Instance.LevelText.text = "Level " + SceneManager.GetActiveScene().name;
        PlayerMove.Instance.enabled = false;
        UIController.Instance.UIMenuGameHide(false);
        cachedCamera = Camera.main;

    }
    public void PauseFunction(bool isPause)
    {
        if (isPause)
        {
            PlayerMove.Instance.enabled = false;
        }
        else
        {
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
        if (Storage.Instance.levelsDones[int.Parse(SceneManager.GetActiveScene().name) - 1] != 1)
        {
            Storage.Instance.money = Storage.Instance.money + 150;
            Storage.Instance.Save();
        }
        else
        {
            UIController.Instance.LevelCoinsWinText.text = "0";
        }
    }
}
