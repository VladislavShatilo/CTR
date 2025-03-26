using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSwitchVert;
    [SerializeField] private GameObject levelSwitchGor;
    [SerializeField] private Canvas levelVertCanvas;
    [SerializeField] private Canvas levelGorCanvas;

    void Start()
    {
        if((float)Screen.width/(float)Screen.height < 1f)
        {
            levelSwitchVert.SetActive(true);
            levelSwitchGor.SetActive(false);
            levelVertCanvas.gameObject.SetActive(true);
            levelGorCanvas.gameObject.SetActive(false);
        }
        else
        {
            levelSwitchVert.SetActive(false);
            levelSwitchGor.SetActive(true);
            levelVertCanvas.gameObject.SetActive(false);
            levelGorCanvas.gameObject.SetActive(true);
        }
    }

    
    
}
