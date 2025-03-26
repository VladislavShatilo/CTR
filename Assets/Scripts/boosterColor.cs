using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class boosterColor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color colorPositive;
    [SerializeField] Color colorNegative;

    public void updateVisual(bool isNegative)
    {
        if (!isNegative)
        {
            image.color = new Color(colorPositive.r, colorPositive.g, colorPositive.b, 0.9f);
        }
        else
        {
            image.color = new Color(colorNegative.r, colorNegative.g, colorNegative.b, 0.9f);
        }

    }
}
