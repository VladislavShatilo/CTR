using TMPro;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] private GameObject planeTop;
    [SerializeField] private GameObject planeDown;
    [SerializeField] private Material[] gateMaterial;
    [SerializeField] private TextMeshProUGUI textSpeed;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(planeTop, nameof(planeTop), this);
        ComponentValidator.CheckAndLog(planeDown, nameof(planeDown), this);
        ComponentValidator.CheckAndLog(gateMaterial, nameof(gateMaterial), this);
        ComponentValidator.CheckAndLog(textSpeed, nameof(textSpeed), this);
    }

    public void UpdateVisual(int gatePower)
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