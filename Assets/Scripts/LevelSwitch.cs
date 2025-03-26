using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelSwitch : MonoBehaviour
{

    [SerializeField] private RectTransform[] buttonsGroups; // массив из 3 кнопок
    [SerializeField] private  float transitionTime = 0.5f;
    [SerializeField] private Image BGimage;
    [SerializeField] private Color[] BGColors;
    [SerializeField] private Button NextSeasonButton;
    [SerializeField] private Button BackSeasonButton;

    private int currentIndex = 0;
    private Vector2 center = Vector2.zero;
    private Vector2 offLeft;
    private Vector2 offRight;

    void Start()
    {
        
        currentIndex = 0;
        NextSeasonButton.onClick.AddListener(delegate { SlideRight(); });
        BackSeasonButton.onClick.AddListener(delegate { SlideLeft(); });

        float screenWidth = Screen.width;
        offLeft = new Vector2(-screenWidth, 0);
        offRight = new Vector2(screenWidth, 0);
        BGimage.color = BGColors[currentIndex];

        for (int i = 0; i < buttonsGroups.Length; i++)
        {
            if (i == currentIndex)
                buttonsGroups[i].anchoredPosition = center;
            else
                buttonsGroups[i].anchoredPosition = offRight;
        }
    }

    public void SlideRight()
    {
        StartCoroutine(SlideRightCor());

    }
    private IEnumerator SlideRightCor()
    {
        int nextIndex = (currentIndex + 1) % buttonsGroups.Length;

        buttonsGroups[currentIndex].DOAnchorPos(offLeft, transitionTime).SetEase(Ease.InOutCubic);

        buttonsGroups[nextIndex].anchoredPosition = offRight;
        buttonsGroups[nextIndex].DOAnchorPos(center, transitionTime).SetEase(Ease.InOutCubic);

        currentIndex = nextIndex;
        Debug.Log(currentIndex);
        NextSeasonButton.enabled = false;
        BackSeasonButton.enabled = false;
        yield return new WaitForSeconds(transitionTime/2);
        BGimage.color = BGColors[currentIndex];
        NextSeasonButton.enabled = true;
        BackSeasonButton.enabled = true;
    }

    public void SlideLeft()
    {
        StartCoroutine(SlideLeftCor());

    }
    private IEnumerator SlideLeftCor()
    {
        int nextIndex = (currentIndex - 1 + buttonsGroups.Length) % buttonsGroups.Length;

        buttonsGroups[currentIndex].DOAnchorPos(offRight, transitionTime).SetEase(Ease.InOutCubic);

        buttonsGroups[nextIndex].anchoredPosition = offLeft;
        buttonsGroups[nextIndex].DOAnchorPos(center, transitionTime).SetEase(Ease.InOutCubic);

        currentIndex = nextIndex;
        Debug.Log(currentIndex);

        NextSeasonButton.enabled = false;
        BackSeasonButton.enabled = false;
        yield return new WaitForSeconds(transitionTime/2);
        BGimage.color = BGColors[currentIndex];
        NextSeasonButton.enabled = true;
        BackSeasonButton.enabled = true;

    }
}
