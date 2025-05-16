using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fade_speed = 2.0f;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(fadeImage, nameof(fadeImage), this);
    }

    private void TransitionFunc()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(TransitionCor());
    }

    public void LoadScenebByName(string nameScene)
    {
        StartCoroutine(LoadScenebByNameCor(nameScene));
    }

    private IEnumerator LoadScenebByNameCor(string nameScene)
    {
        TransitionFunc();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(nameScene);
    }

    public IEnumerator TransitionCor()
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
        while (color.a < 1f)
        {
            color.a += fade_speed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }
}