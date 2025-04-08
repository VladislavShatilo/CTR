using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel; // затемнение + текст
    [SerializeField] private Text tutorialText;

    private int step = 0;
    private bool tutorialCompleted = false;

    public void StartHint()
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = "Нажми на левую часть экрана, чтобы поехать влево";
    }

    void Update()
    {
        if (tutorialCompleted)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Input.mousePosition;

            // Шаг 0: нажал влево
            if (step == 0 && touchPos.x < Screen.width / 2)
            {
                tutorialText.text = "Хорошо! Теперь нажми на правую часть экрана, чтобы поехать вправо";
                step++;
            }
            // Шаг 1: нажал вправо
            else if (step == 1 && touchPos.x >= Screen.width / 2)
            {
                tutorialText.text = "Отлично! Поехали!";
                Invoke(nameof(EndTutorial), 1.5f); // Немного подождем и уберем
                step++;
            }
        }
    }

    void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialCompleted = true;
    }
}
