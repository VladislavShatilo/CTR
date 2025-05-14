using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class wallBreak : MonoBehaviour
{
    [SerializeField] int wallPower;
    [SerializeField] TextMeshProUGUI textWallPower;
    [SerializeField] GameObject bricksEffect;
    private UILevelHUDManager levelHud;

    private void Start()
    {
        levelHud = UILevelManager.Instance.LevelHUD;
        if (levelHud == null)
        {
            Debug.LogError("LevelHud is null");
            enabled = false;
        }
    }

    private void OnValidate()
    {
        if (textWallPower != null)
        {
            textWallPower.text = wallPower.ToString();
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
       

        if (levelHud.GetCurrentPower() >= wallPower)
        {
            Destroy(gameObject);
            levelHud.UpdatePowerText(levelHud.GetCurrentPower() - wallPower);
            Instantiate(bricksEffect, transform.position, transform.rotation);
        }
        else
        {
            PlayerMove.Instance.destroyCar();

        }
    }
}
