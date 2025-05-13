using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class CameraMovementArcade : MonoBehaviour
{
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 8f;

    private Camera cachedCamera;
    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float eyler;

    void Start()
    {
        cachedCamera = Camera.main;
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

            float newX = cachedCamera.transform.position.x + difference.x * speed;
            newX = Mathf.Clamp(newX, minX, maxX);

            cachedCamera.transform.position = new Vector3(newX, cachedCamera.transform.position.y, cachedCamera.transform.position.z);

            eyler += difference.x * 0.03f;
            eyler = Mathf.Clamp(eyler, 160f, 200f);
            cachedCamera.transform.eulerAngles = new Vector3(cachedCamera.transform.eulerAngles.x, eyler, cachedCamera.transform.eulerAngles.z);
        }
    }
}
