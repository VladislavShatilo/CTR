using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] GameObject cylinder;
    void LateUpdate()
    {      
        float scale;
        if (((float)Screen.width / (float)Screen.height) > 1f)
        {          
            scale = 0.02f;
            cylinder.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            scale = 0.015f;
            cylinder.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
