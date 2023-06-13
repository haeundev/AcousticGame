using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Proto.Util;
using UnityEngine;

public class SoundSources : MonoBehaviour
{
    public static SoundSources Instance;
    [SerializeField] private  List<AudioSource> audioSources;
    
    private void Awake()
    {
        Instance = this;
        foreach (var source in audioSources)
        {
            source.gameObject.SetActive(false);
        }
    }

    public static void Play(string name, Action onEnd)
    {
        var sound = Instance.audioSources.FirstOrDefault(p => p.gameObject.name == name);
        if (sound == default)
        {
            Debug.LogError($"Cannot Play {name}. No object found.");
            return;
        }
        sound.gameObject.SetActive(false);
        sound.gameObject.SetActive(true);
        sound.loop = false;
        Debug.Log($"Play sound: {name}");

        Instance.StartCoroutine(WaitForSoundEnd(sound, onEnd));
    }

    private static IEnumerator WaitForSoundEnd(AudioSource sound, Action onEnd)
    {
        yield return new WaitUntil(() => sound.isPlaying == false);
        onEnd?.Invoke();
    }

    public static void PlayLoop(string name)
    {
        var sound = Instance.audioSources.FirstOrDefault(p => p.gameObject.name == name);
        if (sound == default)
        {
            Debug.LogError($"Cannot Play {name}. No object found.");
            return;
        }
        sound.gameObject.SetActive(true);
        sound.loop = true;
        sound.Play();
        Debug.Log($"Play sound loop: {name}.");
    }

    public static void Stop(string name)
    {
        if (name == default || name == string.Empty)
            return;
        var sound = Instance.audioSources.FirstOrDefault(p => p.gameObject.name == name);
        if (sound == default)
        {
            Debug.LogError($"Cannot Stop {name}. No object found.");
            return;
        }
        Debug.Log($"Stop sound: {name}.");
        sound.Stop();
    }
    
    public static void StopAll()
    {
        foreach (var audioSource in Instance.audioSources)
        {
            audioSource.Stop();
        }
    }
}