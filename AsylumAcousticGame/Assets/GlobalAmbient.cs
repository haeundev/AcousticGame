using UnityEngine;

public class GlobalAmbient : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}