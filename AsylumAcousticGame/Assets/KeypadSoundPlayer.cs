using System;
using UnityEngine;

public class KeypadSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource _audioSource;
    public Action<string> OnPressed;
    
    public void Play()
    {
        if (gameObject.GetComponent<AudioSource>() == default)
            _audioSource = gameObject.AddComponent<AudioSource>();
        
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = false;
        _audioSource.playOnAwake = false;
        audioSource.Play();
        OnPressed?.Invoke(name);
    }
}