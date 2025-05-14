using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gate : MonoBehaviour
{
    [SerializeField] private GameObject pointGateEffect;
    [SerializeField] private int power;
    [SerializeField] private GateScript gate1;
    private UILevelHUDManager levelHUDManager;

    void Start()
    {
        levelHUDManager = UILevelManager.Instance.LevelHUD;


    }
    private void OnValidate()
    {
        gate1.updateVisual(power);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (levelHUDManager.GetCurrentPower() + power > 0)
        {
            PlayerMove.Instance.addPower(power);
            ParticleManager.Instance.CreateGateDestroyEffect(pointGateEffect.transform.position);
            Destroy(gameObject);
            levelHUDManager.UpdatePowerText(levelHUDManager.GetCurrentPower() + power);
        }
        else
        {
            PlayerMove.Instance.destroyCar();
        }
    }
}