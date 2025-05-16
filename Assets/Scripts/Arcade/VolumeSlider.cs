using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        ComponentValidator.CheckAndLog(slider, nameof(slider), this);
        slider.value = Storage.Instance.volume;
    }

    public void OnVolumeChanged()
    {
        float volume = slider.value;
        Storage.Instance.volume = volume;
        Storage.Instance.Save();
    }
}