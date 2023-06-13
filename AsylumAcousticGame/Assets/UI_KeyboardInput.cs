using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_KeyboardInput : UI_Window
{
    private CharacterController _cc;
    private string _currentInput;
    [SerializeField] private TMP_InputField inputField;
    public Action OnComplete;
    
    private void Awake()
    {
        _cc = GameObject.Find("--- Player").GetComponentInChildren<CharacterController>();
        inputField.onValueChanged.AddListener(OnValueChanged);
        inputField.onValidateInput += (_, _, c) => char.ToUpper(c) ;
    }

    private void OnValueChanged(string inputText)
    {
        _currentInput = inputText;
        
    }
    
    public void OnClickEnter()
    {
        _passwordCorrectWords.TryGetValue(TaskDirector.Instance.CurrentTask.Group, out var correctWord);
        if (_currentInput.Contains(correctWord))
        {
            gameObject.SetActive(false);
            OnComplete?.Invoke();
        }
    }
    
    public void Quit()
    {
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        base.Open();
        _cc.enabled = false;
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    
    private Dictionary<int, string> _passwordCorrectWords = new()
    {
        {500, "KILL"},
        {600, "DEAF"},
        {700, "LOOK BEHIND"},
    };

    private void OnEnable()
    {
        _cc.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        if (CameraController.Instance != default)
            CameraController.Instance.enabled = false;
    }
    
    private void OnDisable()
    {
        _cc.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        if (CameraController.Instance != default)
            CameraController.Instance.enabled = true;
    }
}
