using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [SerializeField] private GameObject root; 
    [SerializeField] private bool hideOnAwake = true;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float hiddenPositionOffset = 1.5f;
    [SerializeField] private float delay = 0.05f;

    private Vector2 lowPosition;
    private Vector2 highPosition;
    private Vector2 visiblePosition = new();
    private Coroutine ñurrentAnimation;
    private  
    protected virtual void Awake()
    {
        if(root == null || rectTransform == null)
        {
            Debug.LogError("root or rectTransform are null");
            enabled = false;

        }
        if (hideOnAwake)
        {
            HideInstant();
        }
        float screenHeight = Screen.height * hiddenPositionOffset;
        lowPosition = new Vector2(0, -screenHeight);
        highPosition = new Vector2(0, screenHeight);
        rectTransform.anchoredPosition = lowPosition;


    }
    protected void CloseArcadeWindow()
    {
        if (UIArcadeManager.Instance.Windows == null)
        {
            Debug.LogError("UIArcadeManager.Instance.Windows is null");
            enabled = false;
        }
        else
        {
            UIArcadeManager.Instance.Windows.HideTopWindow();
        }
      

    }

    protected void CloseLevelWindow()
    {
        if (UILevelManager.Instance.Windows == null)
        {
            Debug.LogError("UILevelManager.Instance.Windows is null");
            enabled = false;
        }
        else
        {
            UILevelManager.Instance.Windows.HideTopWindow();
        }

    }
    public virtual void Show()
    {
        if (ñurrentAnimation != null)
        {
            StopCoroutine(ñurrentAnimation);
        }

        root.SetActive(true);
        ñurrentAnimation = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        if(UIArcadeManager.Instance.Animation == null)
        {
            Debug.LogError("(UIArcadeManager.Instance.Animation is null");
            enabled = false;
        }
        yield return UIArcadeManager.Instance.Animation.AnimateMove(
            rectTransform,
            visiblePosition,
            delay
        );

        ñurrentAnimation = null;
    }

    public virtual void Hide()
    {
        if (ñurrentAnimation != null)
        {
            StopCoroutine(ñurrentAnimation);
        }

        ñurrentAnimation = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        if (UIArcadeManager.Instance.Animation == null)
        {
            Debug.LogError("(UIArcadeManager.Instance.Animation is null");
            enabled = false;
        }
        yield return UIArcadeManager.Instance.Animation.AnimateMove(
            rectTransform,
            highPosition,
            delay
        );
     
        root.SetActive(false);
        ñurrentAnimation = null;
        rectTransform.anchoredPosition = lowPosition;

    }

    public void HideInstant()
    {
        if (ñurrentAnimation != null)
        {
            StopCoroutine(ñurrentAnimation);
            ñurrentAnimation = null;
        }

    
        root.SetActive(false);
    }
    
}
