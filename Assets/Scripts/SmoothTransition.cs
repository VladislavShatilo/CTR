using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmoothTransition : MonoBehaviour
{
    [SerializeField] private float fade_speed = 1.0f;
    private Image fade_image;

    public IEnumerator StartCor()
    {
        fade_image = GetComponent<Image>();
        
        Color color = fade_image.color;
        color.a = 0f; 
        fade_image.color = color;
        while (color.a < 1f)
        {
            
            color.a += fade_speed * Time.deltaTime;
            fade_image.color = color;
            yield return null;
        }
        color.a = 1f;
        fade_image.color = color;
    }


}
