using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> windows;
    private readonly Stack<UIWindow> windowStack = new();

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
            windowStack.Peek().Hide();
        }

        window.Show();
        windowStack.Push(window);
    }

    public void HideTopWindow()
    {
        if (windowStack.Count == 0) return;

        var window = windowStack.Pop();
        window.Hide();

        if (windowStack.Count > 0)
            windowStack.Peek().Show();
    }

    public T GetWindow<T>() where T : UIWindow
    {
        var window = windows.Find(w => w is T);

        return (T)(window);
    }
}