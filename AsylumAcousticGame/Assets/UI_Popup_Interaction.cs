using UnityEngine;

public class UI_Popup_Interaction : UI_Window
{
    private GameObject _source;
    
    public override void Open()
    {
        base.Open();
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (TaskDirector.Instance.CurrentTask.Group)
            {
                case 100:
                    SoundSources.Stop(TaskDirector.Instance.CurrentTask.SoundStop);
                    TaskDirector.Instance.CompleteCurrentTask();
                    Destroy(_source);
                    gameObject.SetActive(false);
                    CloseScreen.Show();
                    break;
                case 200:
                case 300:
                case 400:
                    SoundSources.Stop(TaskDirector.Instance.CurrentTask.SoundStop);
                    var keypad = UIWindows.GetWindow(2) as UI_Keypad;
                    keypad.OnComplete += () =>
                    {
                        Destroy(_source);
                    };
                    keypad.Open();
                    gameObject.SetActive(false);
                    break;
                case 500:
                case 600:
                case 700:
                    var keyboardInput = UIWindows.GetWindow(6) as UI_KeyboardInput;
                    keyboardInput.Open();
                    keyboardInput.OnComplete += () =>
                    {
                        TaskDirector.Instance.CompleteCurrentTask();
                        Destroy(_source);
                        gameObject.SetActive(false);
                    };
                    gameObject.SetActive(false);
                    break;
            }
            
            if (TaskDirector.Instance.CurrentTask.Group == 200)
            {
                SoundSources.Stop("S3_PhoneRinging");
            }
        }
    }

    public void SetSource(GameObject source)
    {
        _source = source;
    }
}