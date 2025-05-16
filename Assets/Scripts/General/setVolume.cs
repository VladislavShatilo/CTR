using UnityEngine;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    private void Start()
    {
        float savedVolume = Storage.Instance.volume;
        music.volume = savedVolume;
    }
}