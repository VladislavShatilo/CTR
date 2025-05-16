using UnityEngine;

public class GateBuff : MonoBehaviour
{
    [SerializeField] private GameObject pointGateEffect;
    [SerializeField] private int power;
    [SerializeField] private GateScript gate;
    private UILevelHUDManager levelHUDManager;

    private void Start()
    {
        ComponentValidator.CheckAndLog(UILevelManager.Instance.LevelHUD, nameof(UILevelManager.Instance.LevelHUD), this);
        ComponentValidator.CheckAndLog(pointGateEffect, nameof(pointGateEffect), this);
        ComponentValidator.CheckAndLog(gate, nameof(gate), this);

        levelHUDManager = UILevelManager.Instance.LevelHUD;
    }

    private void OnValidate()
    {
        gate.UpdateVisual(power);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelHUDManager.GetCurrentPower() + power > 0)
        {
            PlayerMove.Instance.AddPower(power);
            ParticleManager.Instance.CreateGateDestroyEffect(pointGateEffect.transform.position);
            Destroy(gameObject);
            levelHUDManager.UpdatePowerText(levelHUDManager.GetCurrentPower() + power);
        }
        else
        {
            PlayerMove.Instance.DestroyCar();
        }
    }
}