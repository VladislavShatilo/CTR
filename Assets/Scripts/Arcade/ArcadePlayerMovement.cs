using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

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
    private Transform carTransform;

   
    private void OnEnable()
    {


        var carProvider = CarProvider.Instance;

        ComponentValidator.CheckAndLog(inputHandler, nameof(inputHandler), this);
        ComponentValidator.CheckAndLog(CarProvider.Instance, nameof(CarProvider.Instance), this);
        ComponentValidator.CheckAndLog(carProvider.Cars[Storage.Instance.selectedCar], nameof(gameObject), this);
        if (!carProvider.Cars[Storage.Instance.selectedCar].TryGetComponent(out carTransform))
        {
            Debug.LogError("ArcadePlayerMovement: carTransform is missing!");
            enabled = false;
        }



    }

    public void OnArcadePaused() => StopCar();

    public void OnArcadeContinued() => StartCar();

    public void OnArcadeRestart() => RestartCar();

    private void LateUpdate()
    {
        UpdateRotation();
        float moveDirection = inputHandler.GetMoveDirectionArcade();

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

    private void UpdateRotation()
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
        carTransform.gameObject.SetActive(isActive);
    }
}