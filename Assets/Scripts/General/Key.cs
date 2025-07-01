using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;       // Клавиша для отслеживания
    public Image keyImage;                      // UI-элемент кнопки (Image)
    public Color normalColor = Color.white;     // Цвет по умолчанию
    public Color highlightColor = Color.yellow; // Цвет при нажатии
    public float highlightScale = 1.2f;         // Масштаб при нажатии
    public float transitionSpeed = 10f;         // Скорость анимации

    private Vector3 originalScale;

    void Start()
    {
        if (keyImage == null) keyImage = GetComponent<Image>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKey(targetKey))
        {
            keyImage.color = Color.Lerp(keyImage.color, highlightColor, Time.deltaTime * transitionSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * highlightScale, Time.deltaTime * transitionSpeed);
        }
        else
        {
            keyImage.color = Color.Lerp(keyImage.color, normalColor, Time.deltaTime * transitionSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * transitionSpeed);
        }
    }
}
