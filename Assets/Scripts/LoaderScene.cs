using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackToMainMenu : MonoBehaviour
{

    [SerializeField] GameObject trans;

    private void Transition()
    {
        trans.SetActive(true);
        StartCoroutine(trans.GetComponent<SmoothTransition>().StartCor());
    }
    public void nextLevel()
    {
        SceneManager.LoadScene((int.Parse(SceneManager.GetActiveScene().name) + 1).ToString());
    }
    public void loadMainMenu1()
    {
        StartCoroutine(loadMainMenu());
    }

    public void loadLevelsMenu1()
    {
        StartCoroutine(loadLevelsMenu());
    }

    public void Arcade1()
    {
        StartCoroutine(Arcade());
    }

    public void shop1()
    {
        StartCoroutine(shop());
    }

    public IEnumerator loadMainMenu()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(0);
    }


    public IEnumerator loadLevelsMenu()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelMenu");
    }

    public IEnumerator Arcade()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Arcade");
    }
    public IEnumerator shop()
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("shop");
    }
 

    public void loadMainMenu2()
    {
        SceneManager.LoadScene(0);
    }

    public void loadIntaractiveMainMenu()
    {
        SceneManager.LoadScene("MainMenuNew");
    }

    public void loadLevelsMenu2()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void Arcade2()
    {
        SceneManager.LoadScene("Arcade");
    }

    public void shop2()
    {
        SceneManager.LoadScene("shop");
    }

    public void dollarShop2()
    {
        SceneManager.LoadScene("DollarShop");
    }

    public void loadLevelScene(string nameScene)
    {
        StartCoroutine(LoadLevelSceneCor(nameScene));
    }
    private IEnumerator LoadLevelSceneCor(string nameScene)
    {
        Transition();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(nameScene);
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
        loadLevelsMenu1();

    }
}
