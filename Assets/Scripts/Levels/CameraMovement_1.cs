using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new(0f, 26f, -35f);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float eulerX = 26f;

    private Transform carTarget;

    private void Start()
    {
        transform.parent = null;
        ComponentValidator.CheckAndLog(PlayerMove.Instance, nameof(PlayerMove.Instance), this);
        carTarget = PlayerMove.Instance.gameObject.transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = carTarget.position + offset;
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed), Quaternion.Euler(eulerX, 0f, 0f));
    }
}