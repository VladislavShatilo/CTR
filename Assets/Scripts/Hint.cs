using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;
using TMPro;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel; // затемнение + текст
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private Image leftBlockImage;
    [SerializeField] private Image rightBlockImage
        ;

    private float pulseSpeed = 5.0f;
    private float minAlphaColor = 0.1f;
    private float maxAlphaColor = 0.35f;
    private Color baseColor;
    private bool isOnLeft = false;
    private bool isEnd = false;
   
    private void OnEnable()
    {
       if (Storage.Instance.isHintShown)
       {
           Destroy(gameObject);
           return;
       }
      

        tutorialPanel.SetActive(true);
        leftBlockImage.gameObject.SetActive(false);
        rightBlockImage.gameObject.SetActive(true);
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(false);
        baseColor = leftButton.image.color;

        if (YG2.envir.language == "ru")
        {
            if (YG2.envir.isMobile)
            {
                tutorialText.text = "Нажмите на левую часть экрана, чтобы\nпоехать влево ";

            }
            else if (YG2.envir.isDesktop)
            {
                tutorialText.text = "Нажмите A, чтобы поехать влево";
            }
        }
        else
        {
            if (YG2.envir.isMobile)
            {
                tutorialText.text = "Press the left side of the screen\nto move left ";

            }
            else if (YG2.envir.isDesktop)
            {
                tutorialText.text = "Press A to move left";
            }
        }
       
    }
    

    void leftButtonFun()
    {
        if (YG2.envir.language == "ru")
        {
            if (YG2.envir.isMobile)
            {
                tutorialText.text = "Отлично! Теперь нажмите на правую часть экрана, чтобы\nпоехать вправо.";
            }
            else if (YG2.envir.isDesktop)
            {
                tutorialText.text = "Отлично! Теперь нажмите D, чтобы поехать вправо.";
            }
        }
        else
        {
            if (YG2.envir.isMobile)
            {
                tutorialText.text = "Nice! Now press the right side of the screen\nto go right.";
            }
            else if (YG2.envir.isDesktop)
            {
                tutorialText.text = "Nice! Now press D to go right.";
            }
        }
       
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(true);
        leftBlockImage.gameObject.SetActive(true);
        rightBlockImage.gameObject.SetActive(false);
        isOnLeft = true;
    }
    void rightButtonFun()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        leftBlockImage.gameObject.SetActive(false);
        rightBlockImage.gameObject.SetActive(false);
        if (YG2.envir.language == "ru")
        {
            tutorialText.text = "Прекрасно!\nПоехали!";

        }
        else
        {
            tutorialText.text = "Great!\nLet's go!";

        }
        isEnd = true;
        Invoke(nameof(EndTutorial), 1.5f); // Немного подождем и уберем
        
    }

    void Update()
    {
        if (!isEnd)
        {
            if (playerGO.transform.position.x < -20)
            {
                leftButtonFun();

            }
            if (playerGO.transform.position.x > 20 && isOnLeft)
            {
                rightButtonFun();

            }
            float alpha = Mathf.Lerp(minAlphaColor, maxAlphaColor, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            Color color = baseColor;
            color.a = alpha;
            leftButton.image.color = color;
            rightButton.image.color = color;
        }
      
       
    }

    void EndTutorial()
    {
        //FindObjectOfType<ArcadeManager>().ContinueRoadGen();
       // Destroy(gameObject);
        tutorialPanel.SetActive(false);

    }
}
