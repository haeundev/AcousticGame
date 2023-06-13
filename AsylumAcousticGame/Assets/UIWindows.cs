using System;
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

    private void Start()
    {
        GetWindow(0).Open();
    }

    public static UI_Window GetWindow(int id)
    {
        if (_windows == default)
            return default;
        var found = _windows.First(windowComp => windowComp.ID == id);
        if (found == default)
        {
            Debug.LogError($"Can't find window of ID: {id}");
            return default;
        }
        return found;
    }

    public static void CloseAll()
    {
        foreach (var window in _windows)
        {
            window.gameObject.SetActive(false);
        }
    }
}