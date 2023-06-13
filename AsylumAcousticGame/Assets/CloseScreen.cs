using System;
using UnityEngine;

public class CloseScreen : MonoBehaviour
{
    public static CloseScreen Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        GetComponentInChildren<CameraFadeEffect>().onComplete += () => gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UIWindows.CloseAll();
    }

    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }
}