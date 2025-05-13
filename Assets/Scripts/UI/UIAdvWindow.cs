using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAdvWindow : UIWindow
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button watchAdvButton;
    void Start()
    {
        closeButton.onClick.AddListener(OnClose);
        watchAdvButton.onClick.AddListener(OnReward);
    }
     private IEnumerator OnCloseCor()
    {
        CloseWindow();
        yield return new WaitForSeconds(0.35f);
        UIManager.Instance.Windows.ShowWindow<UIArcadeFinalWindow>();
        UIManager.Instance.Windows.GetWindow<UIArcadeFinalWindow>().CountScore();
    }
    private void OnClose()
    {
        StartCoroutine(OnCloseCor());
    }
    private void OnReward()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
