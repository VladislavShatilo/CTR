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
   
}
