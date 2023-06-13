using System;
using UniRx;
using UnityEngine;

public class CloseScreen : MonoBehaviour
{
    public static CloseScreen Instance { get; private set; }
    public bool IsClosing { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        GetComponentInChildren<CameraFadeEffect>().onComplete += () =>
        {
            IsClosing = false;
            Observable.Timer(TimeSpan.FromSeconds(0.3f)).Subscribe(_ =>
            {
                gameObject.SetActive(false);
            });
        };
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UIWindows.CloseAll();
    }

    public static void Show()
    {
        Instance.IsClosing = true;
        Instance.gameObject.SetActive(true);
    }
}