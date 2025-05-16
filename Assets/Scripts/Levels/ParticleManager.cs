using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject carDestroyEffect;
    [SerializeField] private GameObject gateDestroyEffect;

    public static ParticleManager Instance { get; private set; }

    private void Awake()
    {
        ComponentValidator.CheckAndLog(carDestroyEffect, nameof(carDestroyEffect), this);
        ComponentValidator.CheckAndLog(carDestroyEffect, nameof(carDestroyEffect), this);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CreateCarDestroyEffect(Vector3 pos)
    {
        Instantiate(carDestroyEffect, pos, Quaternion.identity);
    }

    public void CreateGateDestroyEffect(Vector3 pos)
    {
        Instantiate(gateDestroyEffect, pos, Quaternion.identity);
    }
}