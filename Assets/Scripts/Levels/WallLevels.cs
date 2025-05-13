using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLevels : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMove.Instance.destroyCar();
    }
}
