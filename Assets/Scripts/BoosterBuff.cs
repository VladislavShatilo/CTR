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
                other.attachedRigidbody.GetComponent<ArcadePlayerMovement>().AddSpeed(250);
                other.attachedRigidbody.GetComponent<ArcadePlayerMovement>().SetCameraView(80);
            }
            else
            {
                other.attachedRigidbody.GetComponent<ArcadePlayerMovement>().AddSpeed(-100);
                other.attachedRigidbody.GetComponent<ArcadePlayerMovement>().SetCameraView(45);
            }
            Destroy(gameObject);
        }
    }
}
