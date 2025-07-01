using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    public bool isLeftSide = true;               // true Ч лева€ часть экрана, false Ч права€
    public Image buttonImage;                    // UI Image кнопки (иконка лево/право)
    public Color normalColor = Color.white;      // ÷вет по умолчанию
    public Color highlightColor = Color.cyan;    // ÷вет при клике
    public float highlightScale = 1.2f;          // ”величение при клике
    public float transitionSpeed = 10f;          // —корость анимации

    private Vector3 originalScale;
    private bool isPressed = false;

    void Start()
    {
        if (buttonImage == null) buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        isPressed = false;

        if (Input.GetMouseButton(0)) // Ћева€ кнопка мыши
        {
            Vector2 mousePosition = Input.mousePosition;
            float halfWidth = Screen.width / 2f;

            if ((isLeftSide && mousePosition.x < halfWidth) ||
                (!isLeftSide && mousePosition.x >= halfWidth))
            {
                isPressed = true;
            }
        }

        // јнимаци€ цвета и масштаба
        Color targetColor = isPressed ? highlightColor : normalColor;
        Vector3 targetScale = isPressed ? originalScale * highlightScale : originalScale;

        buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime * transitionSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
    }
}
