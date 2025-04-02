using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class wallBreak : MonoBehaviour
{
    [SerializeField] int wallPower;
    [SerializeField] Text textWallPower;
    [SerializeField] GameObject bricksEffect;
    private Text powerText;

    private void OnValidate()
    {
        if (textWallPower != null)
        {
            textWallPower.text = wallPower.ToString();
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        powerText = UIController.Instance.PowerText;

        if (int.Parse(powerText.text) >= wallPower)
        {
            Destroy(gameObject);
            powerText.text = (int.Parse(powerText.text) - wallPower).ToString();
            Instantiate(bricksEffect, transform.position, transform.rotation);
        }
        else
        {
            PlayerMove.Instance.destroyCar();

        }
    }
}
