using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Apin.DialogueSystem;
using Proto.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskDirector : MonoBehaviour
{
    public static TaskDirector Instance;
    public TaskInfos taskInfos;
    private List<TaskInfo> _tasks;
    public TaskInfo CurrentTask { get; private set; }
    private event Action<TaskInfo> OnTaskAcquired;

    private void Awake()
    {
        Instance = this;
        _tasks = taskInfos.Values;
        
        OnTaskAcquired += InitTask;
        
        
        CurrentTask = _tasks.First();
        OnTaskAcquired?.Invoke(CurrentTask);
    }

    private void InitTask(TaskInfo taskInfo)
    {
        UI_Window win;
        switch (taskInfo.TaskType)
        {
            case "display_title":
                win = UIWindows.GetWindow(1) as UI_Toast_Title;
                ((UI_Toast_Title)win).SetTitle(taskInfo.ValueStr);
                win.Open();
                StartCoroutine(WaitForSec(3.5f, CompleteCurrentTask));
                break;
            
            case "silence":
                StartCoroutine(WaitForSec(2, CompleteCurrentTask));
                break;
            
            case "play_sound":
                SoundSources.Play(taskInfo.ValueStr, CompleteCurrentTask);
                break;
            
            case "play_sound_loop":
                SoundSources.PlayLoop(taskInfo.ValueStr);
                break;
            
            case "ui_keypad":
                win = UIWindows.GetWindow(2);
                win.Open();
                break;
            
            case "ui_password":
                win = UIWindows.GetWindow(3);
                win.Open();
                break;
            
            case "ui_popup_interaction":
                win = UIWindows.GetWindow(4);
                win.Open();
                break;
            
            case "door_open":
                
                break;
        }
    }

    private IEnumerator WaitForSec(float sec, Action onEnd)
    {
        yield return new WaitForSeconds(sec);
        onEnd?.Invoke();
    }
    
    public void ResetToFirstTaskOfGroup()
    {
        SoundSources.StopAll();
        
        var first = _tasks.FirstOrDefault(p => p.Group == CurrentTask.Group);
        CurrentTask = first;
        OnTaskAcquired?.Invoke(CurrentTask);

        StartCoroutine(SpawnPlayer(first.Group.ToString()));
    }
    
    private void Update()
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

    private void ShowHint()
    {
        
    }

    public void CompleteCurrentTask()
    {
        StartCoroutine(RunEndActions(() =>
        {
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

            case "spawn":
                yield return SpawnPlayer(values[1]);
                break;

            case "end_game":
                EndGame();
                break;
        }

        yield break;
    }

    private IEnumerator SpawnPlayer(string value)
    {
        var posObj = GameObject.Find(value);
        if (posObj == default)
        {
            Debug.LogError($"Can't find position {value}");
            yield break;
        }
        var pos = posObj.transform.position;
        var player = GameObject.FindWithTag("Player");
        player.transform.position = pos;
        player.transform.rotation = posObj.transform.rotation;
        
        yield break;
    }

    private void EndGame()
    {
        SceneManager.LoadSceneAsync("EndingScene");
    }
}