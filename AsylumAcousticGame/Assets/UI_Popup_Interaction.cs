using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Interaction : UI_Window
{
    public override void Open()
    {
        base.Open();
        
        gameObject.SetActive(true);
    }
    
    

}
