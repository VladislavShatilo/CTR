using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement: MonoBehaviour
{
    [SerializeField] private Vector3 offset = new(0f, 26f, -35f);
    [SerializeField] private float followSpeed = 5f;

    private Transform carTarget;

    void Start()
    {
        transform.parent = null;
        carTarget = PlayerMove.Instance.gameObject.transform;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = carTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Euler(26f, 0f, 0f); 
    }
   
}
