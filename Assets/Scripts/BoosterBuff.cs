using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosterBuff : MonoBehaviour
{
    [SerializeField] bool isNegative;
    [SerializeField] List<GameObject> boosterModels;

    private void OnValidate()
    {
        if(isNegative)
        {
            boosterModels[0].SetActive(false);
            boosterModels[1].SetActive(true);
        }
        else
        {
            boosterModels[0].SetActive(true);
            boosterModels[1].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.GetComponent<ArcadePlayerMovement>())
        {
            if (!isNegative)
            {
                ArcadeManager.Instance.AddSpeed(250);
                Camera.main.fieldOfView = 80;
            }
            else
            {
                ArcadeManager.Instance.AddSpeed(-100);
                Camera.main.fieldOfView = 45;

            }
            Destroy(gameObject);
        }
    }
}
