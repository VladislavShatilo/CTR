using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource inGameMusic;
    [SerializeField] ArcadePlayerMovement arcadePlayerMovement;
    
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
    
    public void advMusic()
    {
        if (arcadePlayerMovement.enabled == true)
        {
            inGameMusic.UnPause();
        }
        else
        {
            menuMusic.UnPause();
        }
    }
}
