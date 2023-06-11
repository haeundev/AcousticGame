using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInteractionPopupOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var win = UIWindows.GetWindow(4) as UI_Popup_Interaction;
            win.SetSource(gameObject);
            win.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var win = UIWindows.GetWindow(4) as UI_Popup_Interaction;
            win.gameObject.SetActive(false);
        }
    }
}