using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{
    [SerializeField] int rotationSpeed;
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }

}
