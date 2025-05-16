using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }
}