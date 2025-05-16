using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPingPong : MonoBehaviour
{
    [SerializeField] private float pulseSpeed = 1.0f;
    [SerializeField] private float minSize = 1.0f;
    [SerializeField] private float maxSize = 1.5f;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ComponentValidator.CheckAndLog(text, nameof(text), this);
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
        float size = Mathf.SmoothStep(minSize, maxSize, t);
        text.fontSize = (int)size;
    }
}