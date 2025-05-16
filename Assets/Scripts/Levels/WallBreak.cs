using TMPro;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    [SerializeField] private int wallPower;
    [SerializeField] private TextMeshProUGUI textWallPower;
    [SerializeField] private GameObject bricksEffect;
    private UILevelHUDManager levelHud;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(textWallPower, nameof(textWallPower), this);
        ComponentValidator.CheckAndLog(bricksEffect, nameof(bricksEffect), this);
    }

    private void Start()
    {
        levelHud = UILevelManager.Instance.LevelHUD;
        ComponentValidator.CheckAndLog(levelHud, nameof(levelHud), this);
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
            PlayerMove.Instance.DestroyCar();
        }
    }
}