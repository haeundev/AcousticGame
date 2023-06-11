using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndDisappearOnCollide : MonoBehaviour
{
    [SerializeField] private string soundName;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundSources.Play(soundName, default);
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        throw new NotImplementedException();
    }
}
