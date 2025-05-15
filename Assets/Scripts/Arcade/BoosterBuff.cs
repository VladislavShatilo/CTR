using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] 
public class boosterBuff : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool isNegative;
   


    [Header("Boost Effects")]
    [SerializeField] private float speedModifier = 250f;
    [SerializeField] private float negativeSpeedModifier = -100f;
    [SerializeField] private float fovEffect = 80f;
    [SerializeField] private float negativeFovEffect = 45f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        ComponentValidator.CheckAndLog(cam, nameof(cam), this);

    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ComponentValidator.CheckAndLog(ArcadeManager.Instance, nameof(ArcadeManager.Instance), this);
            var arcadeManager = ArcadeManager.Instance;

            if (!isNegative)
            {
                arcadeManager.ModifyRoadSpeed(250);
                cam.fieldOfView = 80;
            }
            else
            {
                arcadeManager.ModifyRoadSpeed(-100);
                cam.fieldOfView = 45;

            }
            Destroy(gameObject);
        }
    }
}
