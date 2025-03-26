using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class OpenColorMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] canvases; // массив из 3 кнопок
    [SerializeField] private float transitionTime = 0.5f;
        [SerializeField] private Button OpenColorButton;
    [SerializeField] private Button CloseColorButton;

    private int currentIndex = 0;
    private Vector2 center = Vector2.zero;
    private Vector2 offLeft;
    private Vector2 offRight;

    void Start()
    {
        currentIndex = 0;
        OpenColorButton.onClick.AddListener(delegate { SlideRight(); });
        CloseColorButton.onClick.AddListener(delegate { SlideLeft(); });

        float screenWidth = Screen.width;
        offLeft = new Vector2(-screenWidth, 0);
        offRight = new Vector2(screenWidth, 0);
        // Ставим все кнопки вне экрана, кроме активной
        for (int i = 0; i < canvases.Length; i++)
        {
            if (i == currentIndex)
                canvases[i].anchoredPosition = center;
            else
                canvases[i].anchoredPosition = offRight;
        }
    }
    public void SlideRight()
    {
        StartCoroutine(SlideRightCor());

    }
    private IEnumerator SlideRightCor()
    {
        FindObjectOfType<CarShop>().UpdateUI();
        int nextIndex = (currentIndex + 1) % canvases.Length;

        // текущая уходит влево
        canvases[currentIndex].DOAnchorPos(offLeft, transitionTime).SetEase(Ease.InOutCubic);

        // следующая выезжает справа
        canvases[nextIndex].anchoredPosition = offRight;
        canvases[nextIndex].DOAnchorPos(center, transitionTime).SetEase(Ease.InOutCubic);

        currentIndex = nextIndex;
       
        OpenColorButton.enabled = false;
        CloseColorButton.enabled = false;
        yield return new WaitForSeconds(transitionTime / 2);
        OpenColorButton.enabled = true;
        CloseColorButton.enabled = true;
    }

    public void SlideLeft()
    {
        StartCoroutine(SlideLeftCor());

    }
    private IEnumerator SlideLeftCor()
    {
        int nextIndex = (currentIndex - 1 + canvases.Length) % canvases.Length;

        canvases[currentIndex].DOAnchorPos(offRight, transitionTime).SetEase(Ease.InOutCubic);

        canvases[nextIndex].anchoredPosition = offLeft;
        canvases[nextIndex].DOAnchorPos(center, transitionTime).SetEase(Ease.InOutCubic);

        currentIndex = nextIndex;

        OpenColorButton.enabled = false;
        CloseColorButton.enabled = false;
        yield return new WaitForSeconds(transitionTime / 2);
        OpenColorButton.enabled = true;
        CloseColorButton.enabled = true;

    }
}
