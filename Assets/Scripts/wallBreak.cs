using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class wallBreak : MonoBehaviour
{
    [SerializeField] int wallPower;
    [SerializeField] TextMeshProUGUI textWallPower;
    [SerializeField] GameObject bricksEffect;
    private TextMeshProUGUI powerText;

    private void OnValidate()
    {
        textWallPower.text = wallPower.ToString();
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
