using UnityEngine;

public class UIArcadeManager : UIManager
{
    [SerializeField] private UIArcadeHUDManager arcadeHudManager;
    [SerializeField] private UIMenuManager menuManager;

    public UIArcadeHUDManager ArcadeHUD => arcadeHudManager;
    public UIMenuManager MenuManager => menuManager;
    public static UIArcadeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ComponentValidator.CheckAndLog(arcadeHudManager, nameof(arcadeHudManager), this);
        ComponentValidator.CheckAndLog(menuManager, nameof(menuManager), this);

        arcadeHudManager.Initialize();
        menuManager.Initialize();
    }
}