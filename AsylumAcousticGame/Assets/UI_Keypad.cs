using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Keypad : UI_Window
{
    private string _accumulatedNumbers;
    
    private void Awake()
    {
        var buttons = GetComponentsInChildren<KeypadSoundPlayer>().ToList();
        foreach (var button in buttons)
        {
            button.OnPressed += OnButtonPressed;
        }
    }

    private void OnButtonPressed(string number)
    {
        _accumulatedNumbers += number;

        if (_accumulatedNumbers.Contains("9874"))
        {
            gameObject.SetActive(false);
            TaskDirector.Instance.CompleteCurrentTask();
        }
    }

    public override void Open()
    {
        base.Open();
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if (CameraController.Instance != default)
            CameraController.Instance.enabled = false;
        if (UIWindows.GetWindow(4) != default)
            UIWindows.GetWindow(4).enabled = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        
        if (CameraController.Instance != default)
            CameraController.Instance.enabled = true;
        if (UIWindows.GetWindow(4) != default)
            UIWindows.GetWindow(4).enabled = true;
    }

    public void Quit()
    {
        gameObject.SetActive(false);
        SoundSources.PlayLoop("S3_PhoneMix");
    }
}
