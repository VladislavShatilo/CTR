using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class CameraMovementArcade : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float dragSpeed = 0.05f;
    [SerializeField] private float minXPosition = -7f;
    [SerializeField] private float maxXPosition = 8f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0.03f;
    [SerializeField] private float minRotation = 160f;
    [SerializeField] private float maxRotation = 200f;

    private Camera cachedCamera;
    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float euler;

    void Start()
    {
        cachedCamera = Camera.main;
        ComponentValidator.CheckAndLog(cachedCamera, nameof(cachedCamera), this);
        ComponentValidator.CheckAndLog(EventSystem.current, nameof(EventSystem.current), this);

    }

    void LateUpdate()
    {
       
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = Input.mousePosition - dragOrigin;
            dragOrigin = Input.mousePosition;

            float newX = cachedCamera.transform.position.x + difference.x * dragSpeed;
            newX = Mathf.Clamp(newX, minXPosition, maxXPosition);

            cachedCamera.transform.position = new Vector3(newX, cachedCamera.transform.position.y, cachedCamera.transform.position.z);

            euler += difference.x * rotationSpeed;
            euler = Mathf.Clamp(euler, minRotation, minRotation);
            cachedCamera.transform.eulerAngles = new Vector3(cachedCamera.transform.eulerAngles.x, euler, cachedCamera.transform.eulerAngles.z);
        }
    }
}
