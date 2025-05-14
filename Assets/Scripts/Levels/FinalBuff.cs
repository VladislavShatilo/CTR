using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBuff : MonoBehaviour
{
    public float powerForLevel = 60f;

    [SerializeField] private ParticleSystem[] confettiEffectParticles;
    
    [SerializeField] private Sprite goodStarSprite;

    private UIFinalLevelWindow finalWindow;

    private float durationInSeconds = 1f;
    private float timer;
    private int starsEarned = 0;

    void Start()
    {
    
        enabled = false;
        timer = 0f;
       
        finalWindow = UILevelManager.Instance.Windows.GetWindow<UIFinalLevelWindow>();

        finalWindow.UpdatePowerText(0);
        finalWindow.SetActiveButtons(false);


    }
    IEnumerator FinalWindowUICor()
    {
        yield return new WaitForSeconds(1f);

        UILevelManager.Instance.Windows.ShowWindow<UIFinalLevelWindow>();

        float finPower;
        float progress;
        while (timer < durationInSeconds)
        {
            progress = timer / durationInSeconds;
            finPower = Mathf.RoundToInt(Mathf.Lerp(0, UILevelManager.Instance.LevelHUD.GetCurrentPower(), progress));
            finalWindow.UpdatePowerText((int)finPower);
           

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        finPower = UILevelManager.Instance.LevelHUD.GetCurrentPower();
        finalWindow.UpdatePowerText((int)finPower);

        yield return new WaitForSeconds(0.3f);

        yield return finalWindow.ShowStars(starsEarned,goodStarSprite);


        if (Storage.Instance.levelsDones[int.Parse(SceneManager.GetActiveScene().name) - 1] != 1)
        {
            int level = int.Parse(SceneManager.GetActiveScene().name);
            int moneyAmount = 0;
            if (level >= 1 && level <= 12)
            {
                moneyAmount = Storage.Instance.Season1Money;
            }
            else if (level >= 13 && level <= 24)
            {
                moneyAmount = Storage.Instance.Season2Money;

            }
            else
            {
                moneyAmount = Storage.Instance.Season3Money;
            }

            while (timer < durationInSeconds)
            {
                float progress1 = timer / durationInSeconds;
                float finCoin1 = Mathf.RoundToInt(Mathf.Lerp(0, moneyAmount, progress1));
                finalWindow.UpdateCoinsText((int)finCoin1);
                timer += Time.deltaTime;
                yield return null;
            }

            finalWindow.UpdateCoinsText((int)moneyAmount);

        }
        else 
        {
            finalWindow.UpdateCoinsText(0);
        }

        finalWindow.SetActiveButtons(true);

        if ((int.Parse(SceneManager.GetActiveScene().name) == 12 || 
            int.Parse(SceneManager.GetActiveScene().name) == 24 ||
            int.Parse(SceneManager.GetActiveScene().name) == 36) &&
           Storage.Instance.stars < seasonStars[getNumberOfSeason()]) 
        {
           finalWindow.BlockNextButtonOnLastLevel();
        }
    }

    private int[] seasonStars = new[] { 25, 52 };

    int getNumberOfSeason()
    {
        return int.Parse(SceneManager.GetActiveScene().name) / 12 - 1;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.GetComponent<PlayerMove>())
        {
            enabled = true;
            PlayerMove.Instance.enabled = false;
            FindObjectOfType<Gamemanager>().StopCamera();
            for(int i= 0;i<confettiEffectParticles.Length; i++)
            {
                confettiEffectParticles[i].Play();

            }

            FindObjectOfType<Gamemanager>().EarnMoneyForLevel();
            UpdateStarsInFinalWindow();
            StartCoroutine(FinalWindowUICor());          
            UpdateStarsMenu();

            Storage.Instance.levelsDones[int.Parse(SceneManager.GetActiveScene().name) -1] = 1;
            Storage.Instance.Save();
            UILevelManager.Instance.LevelHUD.AllUIActive(false);

        }
    }

   
    private void UpdateStarsInFinalWindow()
    {
        float progressProcces =(float)UILevelManager.Instance.LevelHUD.GetCurrentPower() / powerForLevel;
        if ( progressProcces >= 0.66f) 
        {
            starsEarned = 3;
        }
        else if (progressProcces >= 0.33f && progressProcces < 0.66f )
        {
            starsEarned = 2;
        }
        else if (progressProcces >= 0 && progressProcces < 0.33f)
        {
            starsEarned = 1;
        }
          
    }

    private void UpdateStarsMenu()
    {
        float progressProcces = (float)UILevelManager.Instance.LevelHUD.GetCurrentPower() / powerForLevel;

        if (progressProcces >= 0.66f && (Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] < 3)) 
        {
              Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] = 3; 
        }
        else if (progressProcces >= 0.33f && progressProcces < 0.66f && (Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] < 2))
        {
            Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] = 2;
        }
        else if (progressProcces >= 0 && progressProcces < 0.33f)
        {
            Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] = 1;
        }
        Storage.Instance.Save();
    }
}
