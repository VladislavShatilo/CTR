using UnityEngine;

public class WallLevels : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMove.Instance.DestroyCar();
    }
}