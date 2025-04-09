using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel; // затемнение + текст
    [SerializeField] private Text tutorialText;
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
    bool IsMobile()
    {
        return SystemInfo.deviceType == DeviceType.Handheld;
    }
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

        if (IsMobile())
        {
            tutorialText.text = "Press the left side of the screen\nto move left ";

        }
        else
        {
            tutorialText.text = "Press A to move left";
        }
    }
    

    void leftButtonFun()
    {
        if (IsMobile())
        {
            tutorialText.text = "Nice! Now press the right side of the screen\nto go right.";
        }
        else
        {
            tutorialText.text = "Nice! Now press D to go right.";
        }
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(true);
        leftBlockImage.gameObject.SetActive(true);
        rightBlockImage.gameObject.SetActive(false);
    }
    void rightButtonFun()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        leftBlockImage.gameObject.SetActive(false);
        rightBlockImage.gameObject.SetActive(false);
        tutorialText.text = "Great!\nLet's go!";
        Invoke(nameof(EndTutorial), 1.5f); // Ќемного подождем и уберем
        
    }

    void Update()
    {
        if(playerGO.transform.position.x < -20)
        {
            leftButtonFun();

        }
        if (playerGO.transform.position.x > 20)
        {
            rightButtonFun();

        }
        float alpha = Mathf.Lerp(minAlphaColor, maxAlphaColor, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        Color color = baseColor;
        color.a = alpha;
        leftButton.image.color = color;
        rightButton.image.color = color;
       
    }

    void EndTutorial()
    {
        FindObjectOfType<ArcadeManager>().ContinueRoadGen();
       // Destroy(gameObject);
        tutorialPanel.SetActive(false);

    }
}
