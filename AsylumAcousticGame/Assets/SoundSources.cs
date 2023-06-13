using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundSources : MonoBehaviour
{
    public static SoundSources Instance;
    private static List<AudioSource> _sources;
    
    private void Awake()
    {
        Instance = this;
        _sources = GetComponentsInChildren<AudioSource>().ToList();
        foreach (var source in _sources)
        {
            source.gameObject.SetActive(false);
        }
    }

    public static void Play(string name, Action onEnd)
    {
        var sound = _sources.FirstOrDefault(p => p.gameObject.name == name);
        if (sound == default)
        {
            Debug.LogError($"Cannot Play {name}. No object found.");
            return;
        }
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
        var sound = _sources.FirstOrDefault(p => p.gameObject.name == name);
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
        var sound = _sources.FirstOrDefault(p => p.gameObject.name == name);
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
        foreach (var sound in _sources)
        {
            sound.Stop();
        }
    }
}