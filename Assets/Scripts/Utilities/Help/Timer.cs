using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static int _timersCounter;

    public readonly List<float> TimeMoments = new List<float>();

    public readonly List<Action> Actions = new List<Action>();

    private float _localTimer;

    private int _localActionCounter;

    private bool _isStarted;

    private Timer()
    {

    }

    public static Timer CreateTimer(string name = "Timer")
    {
        var timer = new GameObject();
        _timersCounter++;
        timer.name = name + _timersCounter;
        timer.AddComponent<Timer>();
        return timer.GetComponent<Timer>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (_isStarted)
        {
            if (_localTimer >= TimeMoments[_localActionCounter])
            {
                if (Actions[_localActionCounter] != null)
                {
                    Actions[_localActionCounter]();
                }
                _localActionCounter++;
                if (_localActionCounter == Actions.Count)
                {
                    Destroy(gameObject);
                }
            }
            _localTimer += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        _isStarted = true;
    }

    public void AddAction(Action action, float time)
    {
        Actions.Add(action);
        TimeMoments.Add(time);
    }

    public void StopTimer()
    {
        Destroy(gameObject);
    }
}
