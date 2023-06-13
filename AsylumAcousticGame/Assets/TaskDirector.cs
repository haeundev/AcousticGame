using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Apin.DialogueSystem;
using Proto.Data;
using UnityEngine;

public class TaskDirector : MonoBehaviour
{
    public static TaskDirector Instance;
    public TaskInfos taskInfos;
    private List<TaskInfo> _tasks;
    private TaskInfo _currentTask;

    private void Awake()
    {
        Instance = this;
        _tasks = taskInfos.Values;
        _currentTask = _tasks.First();
    }

    private void Update()
    {
        
    }

    public void CompleteCurrentTask()
    {
        StartCoroutine(RunEndActions(() =>
        {
            _currentTask = _tasks.FirstOrDefault(p => p.ID == _currentTask.ID + 1);
        }));
    }

    public IEnumerator RunEndActions(Action onDone)
    {
        foreach (var endAction in _currentTask.EndActions.SeparateAllParenthesis())
        {
            var values = endAction.Trim('(', ')', ' ').Replace(" ", string.Empty).Split(',');
            if (values.Length == 0)
                yield break;

            yield return DoEndAction(values);
        }
        onDone?.Invoke();
    }

    private IEnumerator DoEndAction(IReadOnlyList<string> values)
    {
        switch (values[0])
        {
            case "stop_sound":
                var taskToStopAudio = _tasks.Find(p => p.ID == int.Parse(values[1]));
                GameObject.Find($"{taskToStopAudio.ValueStr}").GetComponentInChildren<AudioSource>()?.Stop();
                break;
        }
        
        yield break;
    }
}