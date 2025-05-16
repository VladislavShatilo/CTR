using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource inGameMusic;
    [SerializeField] private ArcadePlayerMovement arcadePlayerMovement;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(menuMusic, nameof(menuMusic), this);
        ComponentValidator.CheckAndLog(inGameMusic, nameof(inGameMusic), this);
        ComponentValidator.CheckAndLog(arcadePlayerMovement, nameof(arcadePlayerMovement), this);
    }

    private void Start()
    {
        SetMenuMusicON();
    }

    public void SetMenuMusicON()
    {
        inGameMusic.Stop();
        menuMusic.Play();
    }

    public void SetInGameMusicON()
    {
        menuMusic.Stop();
        inGameMusic.Play();
    }

    public void RestartInGameMusic()
    {
        inGameMusic.Stop();
        inGameMusic.Play();
    }

    public void PauseInGameMusic()
    {
        inGameMusic.Pause();
    }

    public void ContinueInGameMusic()
    {
        inGameMusic.Play();
    }

    public void AdvMusic()
    {
        if (arcadePlayerMovement.enabled)
        {
            inGameMusic.UnPause();
        }
        else
        {
            menuMusic.UnPause();
        }
    }
}