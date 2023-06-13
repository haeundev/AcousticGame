using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Proto.Util;
using UnityEngine;

public class SoundSources : MonoBehaviour
{
    public static SoundSources Instance;
    [SerializeField] private  List<GameObject> audioSources;
    
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
        sound.GetComponentInChildren<AudioSource>().loop = false;
        sound.gameObject.SetActive(false);
        sound.gameObject.SetActive(true);
        Debug.Log($"Play sound: {name}");

        Instance.StartCoroutine(WaitForSoundEnd(sound.GetComponentInChildren<AudioSource>(), onEnd));
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
        sound.GetComponentInChildren<AudioSource>().loop = true;
        sound.GetComponentInChildren<AudioSource>().Play();
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
        sound.SetActive(false);
    }
    
    public static void StopAll(bool includeDoor)
    {
        foreach (var audioSource in Instance.audioSources)
        {
            if (includeDoor == false)
            {
                if (audioSource.name.Contains("door_"))
                    continue;
            }
            audioSource.SetActive(false);
        }
    }
}