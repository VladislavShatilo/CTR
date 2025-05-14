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
        if(levelHudManager = null)
        {
            Debug.LogError("levelHudManager is missing");
            enabled = false;
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        levelHudManager.Initialize();
      
    }
}
