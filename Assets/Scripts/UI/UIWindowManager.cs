using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> windows; // Все окна проекта
    private readonly Stack<UIWindow> windowStack = new (); // История окон
    public void ShowWindow<T>() where T : UIWindow
    {
        var window = windows.Find(w => w is T);
        if (window == null)
        {
            Debug.LogError($"Window {typeof(T)} not found!");
            return;
        }

        if (windowStack.Count > 0)
        {
            windowStack.Peek().Hide(); // Скрыть текущее окно

        }

        window.Show();
        windowStack.Push(window);
    }

    // Закрыть текущее окно
    public void HideTopWindow()
    {
        if (windowStack.Count == 0) return;

        var window = windowStack.Pop();
        window.Hide();

        if (windowStack.Count > 0)
            windowStack.Peek().Show(); // Показать предыдущее
    }

    // Закрыть все окна
    public void HideAll()
    {
        foreach (var window in windows)
            window.Hide();

        windowStack.Clear();
    }
    public T GetWindow<T>() where T : UIWindow
    {
        var window = windows.Find(w => w is T);
     
        return (T)(window);
    }

}
