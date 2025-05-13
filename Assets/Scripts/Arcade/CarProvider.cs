using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarProvider : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    public GameObject[] Cars => cars;
    public static CarProvider Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }
  
}
