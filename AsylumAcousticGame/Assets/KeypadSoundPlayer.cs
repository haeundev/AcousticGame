using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    public Action<string> OnPressed;
    
    public void Play()
    {
        if (gameObject.GetComponent<AudioSource>() == default)
            gameObject.AddComponent<AudioSource>();
        
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.Play();
        OnPressed?.Invoke(name);
    }
}