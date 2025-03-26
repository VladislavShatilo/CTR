using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = Storage.Instance.volume;
    }

    public void OnVolumeChanged()
    {
        float volume = slider.value;
        Storage.Instance.volume = volume;
        //Storage.Instance.Save();
    }
}
