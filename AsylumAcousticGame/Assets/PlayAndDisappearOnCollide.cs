using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndDisappearOnCollide : MonoBehaviour
{
    [SerializeField] private string soundName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundSources.Play(soundName, default);
            Destroy(gameObject);
        }
    }
}