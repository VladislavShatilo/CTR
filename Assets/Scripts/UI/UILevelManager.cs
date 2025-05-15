using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelManager : UIManager
{
    [SerializeField] private UILevelHUDManager levelHudManager;     
    public UILevelHUDManager LevelHUD => levelHudManager;

    public static UILevelManager Instance { get; private set; }
    void Awake()
    {
        ComponentValidator.CheckAndLog(levelHudManager, nameof(levelHudManager), this);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        levelHudManager.Initialize();
      
    }
}
