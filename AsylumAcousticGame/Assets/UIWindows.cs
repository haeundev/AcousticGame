using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIWindows : MonoBehaviour
{
    public static UIWindows Instance;
    private static List<UI_Window> _windows;

    private void Awake()
    {
        Instance = this;
        _windows = GetComponentsInChildren<UI_Window>().ToList();

        foreach (var window in _windows)
        {
            window.gameObject.SetActive(false);
        }
    }

    public static UI_Window GetWindow(int id)
    {
        return _windows.FirstOrDefault(windowComp => windowComp.ID == id);
    }
}