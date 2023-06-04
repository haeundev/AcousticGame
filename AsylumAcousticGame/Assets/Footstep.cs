using UnityEngine;

public class Footstep : MonoBehaviour
{
    private bool _isWalking;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D))
        {
            _isWalking = true;
            _audioSource.enabled = true;
        }
        
        else if (Input.GetKeyUp(KeyCode.W)
                 || Input.GetKeyUp(KeyCode.A)
                 || Input.GetKeyUp(KeyCode.S)
                 || Input.GetKeyUp(KeyCode.D))
        {
            _isWalking = false;
            _audioSource.enabled = false;
        }
    }
}