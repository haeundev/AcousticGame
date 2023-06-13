using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public static EndScreen Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }
}
