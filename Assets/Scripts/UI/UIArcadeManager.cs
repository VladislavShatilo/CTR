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
        ComponentValidator.CheckAndLog(arcadeHudManager, nameof(arcadeHudManager), this);
        ComponentValidator.CheckAndLog(menuManager, nameof(menuManager), this);

        arcadeHudManager.Initialize();
        menuManager.Initialize();
      
    }
}
