using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class InputHandler : MonoBehaviour
{
    [Header("Mobile Settings")]
    [SerializeField] private float mobileMoveMultiplier = 5f;

    [Header("Desktop Settings")]
    [SerializeField] private float keyboardMoveMultiplier = 5f;

    private bool isMobile;
    private float currentMoveDirection;
    private float input;

    private void Awake()
    {
        isMobile = YG2.envir.isMobile;
    }

    public float GetMoveDirectionArcade()
    {
        if (isMobile)
        {
            HandleMobileInputArcade();
        }
        else
        {
            HandleDesktopInputArcade();
        }

        return currentMoveDirection;
    }

    public float GetMoveDirectionLevel()
    {
        if (isMobile)
        {
            HandleMobileInputLevel();
        }
        else
        {
            HandleDesktopInputLevel();
        }
        return input;
    }

    private void HandleMobileInputLevel()
    {
        float targetInput = 0f;
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                targetInput = -1f;
            }
            else
            {
                targetInput = 1f;
            }
        }
        input = Mathf.Lerp(input, targetInput, mobileMoveMultiplier * Time.deltaTime);
    }

    private void HandleDesktopInputLevel()
    {
        input = Input.GetAxis("Horizontal");
    }

    private void HandleMobileInputArcade()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUI())
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                currentMoveDirection = -mobileMoveMultiplier;
            }
            else
            {
                currentMoveDirection = mobileMoveMultiplier;
            }
        }
    }

    private void HandleDesktopInputArcade()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentMoveDirection = -keyboardMoveMultiplier;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentMoveDirection = keyboardMoveMultiplier;
        }
        else
        {
            currentMoveDirection = 0;
        }
    }

    private static bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}