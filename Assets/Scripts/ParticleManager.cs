using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    [SerializeField] private GameObject carDestroyEffect;
    [SerializeField] private GameObject gateDestroyEffect;

    public static ParticleManager Instance { get; private set; }

    void Awake()
    {
        // Проверка, существует ли уже экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject); // Не уничтожать при смене сцены
    }
    public void CreateCarDestroyEffect(Vector3 pos)
    {
        Instantiate(carDestroyEffect, pos,Quaternion.identity);
    }
    public void CreateGateDestroyEffect(Vector3 pos)
    {
        Instantiate(gateDestroyEffect, pos, Quaternion.identity);
    }
   
}
