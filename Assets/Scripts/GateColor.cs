using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GateScript : MonoBehaviour
{
    enum MaterialType
    {
        Blue,
        BlueTrans,
        Red,
        RedTrans
    }

    [SerializeField] GameObject planeTop;
    [SerializeField] GameObject planeDown;
    [SerializeField] Material[] gateMaterial;
    [SerializeField] Text textSpeed;

    public void updateVisual(int gatePower)
    {
        if (gatePower > 0)
        {
            planeTop.GetComponent<Renderer>().material = gateMaterial[(int)MaterialType.Blue];
            planeDown.GetComponent<Renderer>().material = gateMaterial[(int)MaterialType.BlueTrans];        
            textSpeed.text = "+" + gatePower.ToString();
        }
        else
        {
            planeTop.GetComponent<Renderer>().material = gateMaterial[(int)MaterialType.Red];
            planeDown.GetComponent<Renderer>().material = gateMaterial[(int)MaterialType.RedTrans];
            textSpeed.text = gatePower.ToString();
        }

    }
}