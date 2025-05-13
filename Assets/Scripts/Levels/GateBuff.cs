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
    private TextMeshProUGUI powerText;

    void Start()
    {
        
        powerText = UIController.Instance.PowerText;
       
    }
    private void OnValidate()
    {
        gate1.updateVisual(power);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (int.Parse(powerText.text) + power > 0)
        {
            PlayerMove.Instance.addPower(power);
            ParticleManager.Instance.CreateGateDestroyEffect(pointGateEffect.transform.position);
            Destroy(gameObject);
            powerText.text = (int.Parse(powerText.text) + power).ToString();
        }
        else
        {
            PlayerMove.Instance.destroyCar();
        }
    }
}