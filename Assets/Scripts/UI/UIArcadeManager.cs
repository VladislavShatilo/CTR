using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArcadeManager : UIManager
{
    [SerializeField] private UIArcadeHUDManager arcadeHudManager;
    [SerializeField] private UIMenuManager menuManager;

    public UIArcadeHUDManager ArcadeHUD => arcadeHudManager;
    public UIMenuManager MenuManager => menuManager;
    public static UIArcadeManager Instance { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        if(arcadeHudManager == null)
        {
            Debug.LogError("arcadeHudManager is missing");
            enabled = false;
        }
        if(menuManager == null)
        {
            Debug.LogError("menuManager is missing");
            enabled = false;
        }
        arcadeHudManager.Initialize();
        menuManager.Initialize();
      
    }
}
