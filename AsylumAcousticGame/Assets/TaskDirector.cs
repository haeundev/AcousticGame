using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Apin.DialogueSystem;
using Proto.Data;
using UnityEngine;

public class TaskDirector : MonoBehaviour
{
    [SerializeField] private int initialTaskID = 1;
    public static TaskDirector Instance;
    public TaskInfos taskInfos;
    private List<TaskInfo> _tasks;
    public TaskInfo CurrentTask { get; private set; }
    private event Action<TaskInfo> OnTaskAcquired;

    private void Start()
    {
        Instance = this;
        _tasks = taskInfos.Values;
        
        OnTaskAcquired += InitTask;

        CurrentTask = _tasks.First(p => p.ID == initialTaskID);
        OnTaskAcquired?.Invoke(CurrentTask);
        StartCoroutine(SpawnPlayer(CurrentTask.Group.ToString()));
    }

    private void InitTask(TaskInfo taskInfo)
    {
        if (taskInfo == default)
            return;
        switch (taskInfo.TaskType)
        {
            case "display_title":
                DisplayTitle(taskInfo);
                break;
            
            case "play_sound":
                SoundSources.Play(taskInfo.ValueStr, CompleteCurrentTask);
                break;
            
            case "play_sound_loop":
                SoundSources.PlayLoop(taskInfo.ValueStr);
                if (taskInfo.IsInstant)
                    CompleteCurrentTask();
                break;
            
            case "skip":
                CompleteCurrentTask();
                break;
        }
    }

    private void DisplayTitle(TaskInfo taskInfo)
    {
        SoundSources.StopAll();
        StartCoroutine(coDisplayTitle(taskInfo));
    }

    private IEnumerator coDisplayTitle(TaskInfo taskInfo)
    {
        yield return new WaitUntil(() => CloseScreen.Instance.IsClosing == false);
        var win = UIWindows.GetWindow(1) as UI_Toast_Title;
        win.SetTitle(taskInfo.ValueStr);
        win.Open();
        yield return new WaitForSeconds(3f);
        Debug.Log("Title display done.");
        CompleteCurrentTask();
    }

    // private IEnumerator WaitForSec(float sec, Action onEnd)
    // {
    //     yield return new WaitForSeconds(sec);
    //     onEnd?.Invoke();
    // }
    
    public void ResetToFirstTaskOfGroup()
    {
        UIWindows.CloseAll();
        SoundSources.StopAll();
        
        var first = _tasks.FirstOrDefault(p => p.Group == CurrentTask.Group);
        CurrentTask = first;
        OnTaskAcquired?.Invoke(CurrentTask);

        StartCoroutine(SpawnPlayer(first.Group.ToString()));
    }

    private void Update()
    {
        var keyboardInput = UIWindows.GetWindow(6);
        if (keyboardInput.gameObject.activeSelf)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetToFirstTaskOfGroup();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                ShowHint();
            }
        }
    }

    private void ShowHint()
    {
        
    }

    public void CompleteCurrentTask()
    {
        StartCoroutine(RunEndActions(() =>
        {
            if (_tasks == default)
                return;
            if (CurrentTask == default)
                return;
            CurrentTask = _tasks.FirstOrDefault(p => p.ID == CurrentTask.ID + 1);
            OnTaskAcquired?.Invoke(CurrentTask);
        }));
    }

    public IEnumerator RunEndActions(Action onDone)
    {
        foreach (var endAction in CurrentTask.EndActions.SeparateAllParenthesis())
        {
            var values = endAction.Trim('(', ')', ' ').Replace(" ", string.Empty).Split(',');
            if (values.Length == 0)
                yield break;

            yield return DoEndAction(values);
        }
        
        yield return new WaitUntil(() => CloseScreen.Instance.IsClosing == false);
        onDone?.Invoke();
    }

    private IEnumerator DoEndAction(IReadOnlyList<string> values)
    {
        switch (values[0])
        {
            case "stop_sound":
                var taskToStopAudio = _tasks.Find(p => p.ID == int.Parse(values[1]));
                GameObject.Find($"{taskToStopAudio.ValueStr}")?.GetComponentInChildren<AudioSource>()?.Stop();
                break;

            case "spawn":
                UIWindows.GetWindow(1).enabled = false;
                yield return SpawnPlayer(values[1]);
                break;
        }
    }

    private IEnumerator SpawnPlayer(string value)
    {
        var player = GameObject.Find("--- Player");
        var cc = player.GetComponentInChildren<CharacterController>();
        cc.enabled = false;
        var posObj = GameObject.Find(value);
        if (posObj == default)
        {
            Debug.LogError($"Can't find position {value}");
            yield break;
        }
        var pos = posObj.transform.position;
        var rot = posObj.transform.rotation;
        player.transform.SetPositionAndRotation(pos, rot);
        Camera.main.gameObject.AddComponent<ColorScreenFadeInCamera>();
        Debug.Log($"Spawn player to {value}");
        cc.enabled = true;
    }
}