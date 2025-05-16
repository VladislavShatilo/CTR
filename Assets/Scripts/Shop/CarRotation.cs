using UnityEngine;

public class CarRotation : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }
}