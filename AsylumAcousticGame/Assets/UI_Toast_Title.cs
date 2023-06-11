using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Toast_Title : UI_Window
{
    [SerializeField] private TextMeshProUGUI titleText;
    
    public override void Open()
    {
        base.Open();
        
        gameObject.SetActive(true);
    }
    
    public void SetTitle(string title)
    {
        titleText.text = title;
    }
}
