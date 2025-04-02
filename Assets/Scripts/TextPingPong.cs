using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextPingPong : MonoBehaviour
{
    [SerializeField] private float pulseSpeed = 1.0f;
    [SerializeField] private float minSize = 1.0f;
    [SerializeField] private float maxSize = 1.5f;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
        float size = Mathf.SmoothStep(minSize, maxSize, t);
        text.fontSize = (int)size;
    }
}
