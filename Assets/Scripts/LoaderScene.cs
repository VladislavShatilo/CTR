using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;


public class BackToMainMenu : MonoBehaviour
{
   
    [SerializeField] GameObject trans;

    private void Transition()
    {
        trans.SetActive(true);
        StartCoroutine(trans.GetComponent<SmoothTransition>().StartCor());
    }

    public void LoadLevelMenu()
    {
        StartCoroutine(LoadLevelMenuCor());
    }

    public void LoadShop()
    {
        StartCoroutine(LoadShopCor());
    }

    public void LoadScenebByName(string nameScene)
    {
       // YG2.optionalPlatform.FirstInterAdvShow();
        YG2.InterstitialAdvShow();
        StartCoroutine(LoadScenebByNameCor(nameScene));
    }

    public IEnumerator LoadLevelMenuCor()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelMenu");
       // SceneAddressManager.Instance.LoadScene("LevelMenu");
    }

    public IEnumerator LoadShopCor()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Shop");

        // SceneAddressManager.Instance.LoadScene("Shop");
    }

    private IEnumerator LoadScenebByNameCor(string nameScene)
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(nameScene);

       // SceneAddressManager.Instance.LoadScene(nameScene);
    }


    public void nextLevel()
    {
        SceneManager.LoadScene((int.Parse(SceneManager.GetActiveScene().name) + 1).ToString());
    }
    public void loadMainMenu1()
    {
        StartCoroutine(loadMainMenu());
    }

   

    public void Arcade1()
    {
        StartCoroutine(Arcade());
    }

   

    public IEnumerator loadMainMenu()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(0);
    }


    

    public IEnumerator Arcade()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Arcade");
    }
   



  
    
    public void Rastart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelsMenuFromLevel()
    {
        int level = int.Parse(SceneManager.GetActiveScene().name);
        if (level >= 0 && level <= 12)
        {
            // PlayerPrefs.SetInt("ActiveSeason", 1);
            Storage.Instance.activeSeason = 1;
            
        }
        else if (level >= 13 && level <= 24)
        {
            //PlayerPrefs.SetInt("ActiveSeason", 2);
            Storage.Instance.activeSeason = 2;
        }
        else if (level >= 25 && level <= 36)
        {
            //PlayerPrefs.SetInt("ActiveSeason", 3);
            Storage.Instance.activeSeason =3;
        }
        else if (level >= 37 && level <= 48)
        {
           // PlayerPrefs.SetInt("ActiveSeason", 4);
            Storage.Instance.activeSeason = 4;
        }
        else
        {
           // PlayerPrefs.SetInt("ActiveSeason", 5);
            Storage.Instance.activeSeason = 5;
        }
        Storage.Instance.Save();
        LoadLevelMenu();

    }
}
