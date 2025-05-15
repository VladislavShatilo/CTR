using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.XR;
using YG;

public class ArcadePlayerMovement : MonoBehaviour, IArcadeStateListener
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveRange;
    [SerializeField] private int maxSteerRotation;
    [SerializeField] private float steerRotationSpeed;
    [SerializeField] private float rotationMultiplierY = 1.25f;
    [SerializeField] private float rotationMultiplierZ = 0.5f;

    [Header("References")]
    [SerializeField] private InputHandler inputHandler;

    private Vector3 currentPos;
    private Vector3 lastPos;
    private readonly float minMoveDistanceToRotate = 0.02f;
    private float rotation;
    private Renderer carMesh;
    private Transform carTransform;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(inputHandler, nameof(inputHandler), this);

    }

    private void OnEnable()
    {
        ComponentValidator.CheckAndLog(CarProvider.Instance.Cars[Storage.Instance.SelectedCar],
            nameof(gameObject), this);

        if (!CarProvider.Instance.Cars[Storage.Instance.SelectedCar].TryGetComponent(out carMesh))
        {
            Debug.LogError("ArcadePlayerMovement: Car mesh is missing!");
            enabled = false;
        }
        carTransform = carMesh.gameObject.transform;
       
    }

    public void OnArcadePaused() => StopCar();
    public void OnArcadeContinued() => StartCar();
    public void OnArcadeRestart() => RestartCar();

    void LateUpdate()
    {
        UpdateRotation();
        float moveDirection = inputHandler.GetMoveDirection();

        if (Mathf.Abs(moveDirection) > 0.01f)
        {

            float newX = transform.position.x + moveDirection * moveSpeed * Time.deltaTime;
            newX = Mathf.Clamp(newX, -moveRange, moveRange);

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );

        }

    }

    void UpdateRotation()
    {
        currentPos = transform.position;

        if (Vector3.Distance(currentPos, lastPos) > minMoveDistanceToRotate)
        {
            if (currentPos.x > lastPos.x)
            {
                rotation += Time.deltaTime * steerRotationSpeed;
            }
            else if (currentPos.x < lastPos.x)
            {
                rotation += Time.deltaTime * -steerRotationSpeed;
            }
        }
        else
        {
            if (rotation > 0)
            {
                rotation += Time.deltaTime * -steerRotationSpeed * 2;
                if (rotation < 0)
                    rotation = 0;
            }
            else if (rotation < 0)
            {
                rotation += Time.deltaTime * steerRotationSpeed * 2;
                if (rotation > 0)
                    rotation = 0;
            }
        }

        lastPos = currentPos;
        rotation = Mathf.Clamp(rotation, -maxSteerRotation, maxSteerRotation);
        carTransform.localEulerAngles = new Vector3(carTransform.localEulerAngles.x, rotation * rotationMultiplierY, rotation * rotationMultiplierZ);
    }
  
    private void StopCar()
    {
        enabled = false;
    }
    private void StartCar()
    {
        enabled = true;
    }

    public void DestroyCar() => SetCarActive(false);
    public void RestartCar()
    {
      
        SetCarActive(true);
        gameObject.SetActive(true);
      
    }

    public void SetCarActive(bool isActive)
    {
        enabled = isActive;
        carMesh.gameObject.SetActive(isActive);
    }
}
