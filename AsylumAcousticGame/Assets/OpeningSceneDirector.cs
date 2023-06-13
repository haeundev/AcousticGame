using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneDirector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private GameObject globalSoundPlayer;
    private bool _isLoading;
    
    private void Start()
    {
        DontDestroyOnLoad(globalSoundPlayer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isLoading)
                return;
            _isLoading = true;   
            SceneManager.LoadScene("InGame");
            subtitle.text = "Loading...";
        }
    }
}