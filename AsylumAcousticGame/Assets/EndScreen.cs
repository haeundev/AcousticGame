using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public static EndScreen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public static void Show()
    {
        Instance.gameObject.SetActive(true);
        SoundSources.StopAll(true);
    }

    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}