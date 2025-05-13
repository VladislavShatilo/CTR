using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [SerializeField] private GameObject root; 
    [SerializeField] private bool hideOnAwake = true;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float hiddenPositionOffset = 1.5f;

  
    private Vector2 lowPosition;
    private Vector2 highPosition;
    private Vector2 visiblePosition = new();
    private Coroutine ñurrentAnimation;

    protected virtual void Awake()
    {
        if (hideOnAwake)
        {
            HideInstant();
        }
        float screenHeight = Screen.height * hiddenPositionOffset;
        lowPosition = new Vector2(0, -screenHeight);
        highPosition = new Vector2(0, screenHeight);
        rectTransform.anchoredPosition = lowPosition;


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
        yield return UIManager.Instance.Animation.AnimateMove(
            rectTransform,
            visiblePosition,
            0.05f
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
        yield return UIManager.Instance.Animation.AnimateMove(
            rectTransform,
            highPosition,
            0.05f
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
