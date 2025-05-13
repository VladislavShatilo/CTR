using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class InputHandler : MonoBehaviour
{
    [Header("Mobile Settings")]
    [SerializeField] private float mobileMoveMultiplier = 5f;

    [Header("Desktop Settings")]
    [SerializeField] private float keyboardMoveMultiplier = 5f;

    private bool _isMobile;
    private float _currentMoveDirection;

    private void Awake()
    {
        _isMobile = YG2.envir.isMobile;
    }

    public float GetMoveDirection()
    {
        _currentMoveDirection = 0f;

        if (_isMobile)
        {
            HandleMobileInput();
        }
        else
        {
            HandleDesktopInput();
        }

        return _currentMoveDirection;
    }

    private void HandleMobileInput()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUI())
        {
           
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    _currentMoveDirection = -mobileMoveMultiplier;
                }
                else
                {
                    _currentMoveDirection = mobileMoveMultiplier;
                }
            
           
        }
    }

    private void HandleDesktopInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _currentMoveDirection = -keyboardMoveMultiplier;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _currentMoveDirection = keyboardMoveMultiplier;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
