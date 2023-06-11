using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI task;
    
    
    void Update()
    {
        task.text = $"Current task : {TaskDirector.Instance.CurrentTask.ID.ToString()}";
    }
}
