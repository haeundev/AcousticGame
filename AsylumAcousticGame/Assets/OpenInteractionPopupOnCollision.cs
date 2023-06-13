using Proto.Data;
using UnityEngine;

public class OpenInteractionPopupOnCollision : MonoBehaviour
{
    private TaskInfo CurrentTask => TaskDirector.Instance.CurrentTask;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        var win = UIWindows.GetWindow(4) as UI_Popup_Interaction;
        win.SetSource(gameObject);
        win.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        var win = UIWindows.GetWindow(4) as UI_Popup_Interaction;
        win.gameObject.SetActive(false);

        if (CurrentTask.ID == 9) // S3_PhoneMix
        {
            SoundSources.PlayLoop(CurrentTask.SoundStop);
        }
    }
}