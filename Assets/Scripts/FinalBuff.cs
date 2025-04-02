using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBuff : MonoBehaviour
{
    public float powerForLevel = 60f;

    [SerializeField] private ParticleSystem[] confettiEffectParticles;
    
    [SerializeField] private Sprite goodStarSprite;

    private Image[] starImages;
    private Text currentPowerText;
    private Text coinsInLevel;
    private float durationInSeconds = 1f;
    private float timer;
    private int starsEarned = 0;

    void Start()
    {
        starImages = UIController.Instance.StarOnWinWindowImages; 
        coinsInLevel = UIController.Instance.LevelCoinsWinText;
        currentPowerText = UIController.Instance.PowerText;
        enabled = false;
        timer = 0f;
      
        for(int i = 0; i < starImages.Length; i++)
        {
            starImages[i].gameObject.SetActive(true);
        }

        UIController.Instance.FinalPowerWinText.text = "0";
        UIController.Instance.MenuWinButton.gameObject.SetActive(false);
        UIController.Instance.RestartWinButton.gameObject.SetActive(false);
        UIController.Instance.NextWinButton.gameObject.SetActive(false);

    }
    IEnumerator FinalWindowUICor()
    {
        yield return new WaitForSeconds(1f);
      
        UIController.Instance.WinWindow.gameObject.SetActive(true);
        FindObjectOfType<WindowAnimation2>().ToggleMenuOn("FinalWindow");
        coinsInLevel.gameObject.SetActive(true);

        float finPower;
        float progress;
        while (timer < durationInSeconds)
        {
            progress = timer / durationInSeconds;
            finPower = Mathf.RoundToInt(Mathf.Lerp(0, int.Parse(currentPowerText.text), progress));
            UIController.Instance.FinalPowerWinText.text = finPower.ToString();

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        finPower = int.Parse(currentPowerText.text);
        UIController.Instance.FinalPowerWinText.text = finPower.ToString();

     
        yield return new WaitForSeconds(0.3f);

        switch (starsEarned)
        {
            case 1:
                starImages[0].sprite = goodStarSprite;

                break;
            case 2:
                starImages[0].sprite = goodStarSprite;
                yield return new WaitForSeconds(0.2f);

                starImages[1].sprite = goodStarSprite;

                break;
            case 3:
                starImages[0].sprite = goodStarSprite;
                yield return new WaitForSeconds(0.2f);

                starImages[1].sprite = goodStarSprite;
                yield return new WaitForSeconds(0.2f);

                starImages[2].sprite = goodStarSprite;
                break;
        }
        yield return new WaitForSeconds(0.3f);

     


        if (coinsInLevel.text != "0")
        {
            while (timer < durationInSeconds)
            {
                float progress1 = timer / durationInSeconds;
                float finCoin1 = Mathf.RoundToInt(Mathf.Lerp(0, 150, progress1));
                coinsInLevel.text = finCoin1.ToString();
                timer += Time.deltaTime;
                yield return null;
            }

            coinsInLevel.text = "150";
        }

        UIController.Instance.MenuWinButton.gameObject.SetActive(true);
        UIController.Instance.RestartWinButton.gameObject.SetActive(true);
        UIController.Instance.NextWinButton.gameObject.SetActive(true);
        if((int.Parse(Storage.Instance.nameActiveScene) == 12 || 
            int.Parse(Storage.Instance.nameActiveScene) == 24 ||
            int.Parse(Storage.Instance.nameActiveScene) == 36) &&
           Storage.Instance.stars < seasonStars[getNumberOfSeason()]) 
        {
            UIController.Instance.NextWinButton.enabled = false;
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
            Storage.Instance.levelsStars[int.Parse(SceneManager.GetActiveScene().name) - 1] = starsEarned;
            Storage.Instance.Save();

            UIController.Instance.UIMenuGameOff();

        }
    }

   
    private void UpdateStarsInFinalWindow()
    {
        float progressProcces = (float.Parse(currentPowerText.text) / powerForLevel);
        
        if ( progressProcces >= 0.66f) 
        {
            starsEarned = 3;
        }
        else if (progressProcces >= 0.33f && progressProcces < 0.66f)
        {
            starsEarned = 2;
        }
        else if (progressProcces >= 0 && progressProcces < 0.33f){
            starsEarned = 1;
        }
          
    }

    private void UpdateStarsMenu()
    {
        float progressProcces = (float.Parse(currentPowerText.text) / powerForLevel);

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
