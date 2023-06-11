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
        var found = _windows.First(windowComp => windowComp.ID == id);
        if (found == default)
        {
            Debug.LogError($"Can't find window of ID: {id}");
            Application.Quit();
            return default;
        }
        return found;
    }
}