using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIWindowManager windowManager;
    [SerializeField] private UIArcadeHUDManager hudManager;
    [SerializeField] private UIAnimationHandler animationHandler;
    [SerializeField] private UIMenuManager menuManager;
    public UIWindowManager Windows => windowManager;
    public UIAnimationHandler Animation => animationHandler;
    public UIArcadeHUDManager HUD => hudManager;
    public UIMenuManager MenuManager => menuManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        hudManager.Initialize();
        menuManager.Initialize();
    }
 
}
