using System;
using UniRx;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public static EndScreen Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        GetComponentInChildren<CameraFadeEffect>().onComplete += () =>
        {
            Observable.Timer(TimeSpan.FromSeconds(0.3f)).Subscribe(_ =>
            {
                gameObject.SetActive(false);
            });
        };
        gameObject.SetActive(false);
    }
    
    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }
}