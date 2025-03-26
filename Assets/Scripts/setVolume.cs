using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVolume : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    void Start()
    {
        float savedVolume = Storage.Instance.volume;
        music.volume = savedVolume;
    }
}
