using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIWindowManager windowManager;  // Общий для всех сцен
    [SerializeField] private UIAnimationHandler animationHandler;  // Общий для всех сцен

    public UIWindowManager Windows => windowManager;
    public UIAnimationHandler Animation => animationHandler;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(windowManager, nameof(windowManager), this);
        ComponentValidator.CheckAndLog(animationHandler, nameof(animationHandler), this);

    }

}
