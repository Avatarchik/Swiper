using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using TMPro;

public class TimerObject : MonoBehaviour
{
    public TextMeshProUGUI GUITimer;

    public TextMeshPro Timer;

    public DateTime StartTime;

    public TimerType TimerType;

    public TimeSpan TimeSpan;

    public bool TimerActive;

    public event Action Start;

    public event Action Stop;

    void Update()
    {
        if (TimerActive)
        {
            var diff = DateTime.Now - StartTime;
            string time = "";
            if (diff < TimeSpan)
            {
                var res = TimeSpan - diff;

                switch (TimerType)
                {
                    case TimerType.SecondsTimer:
                        time = res.TotalSeconds.ToString("00");
                        break;
                    case TimerType.MinutesTimer:
                        time = (res.Days*24*60 + res.Hours*60 + res.Minutes).ToString("00") + ":" +
                               res.Seconds.ToString("00");
                        break;
                    case TimerType.HoursTimer:
                        time = (res.Days*24 + res.Hours).ToString("00") + ":" + res.Minutes.ToString("00") + ":" +
                               res.Seconds.ToString("00");
                        break;
                }
            }
            else
            {
                TimerActive = false;
                OnStop();
                switch (TimerType)
                {
                    case TimerType.SecondsTimer:
                        time = "0";
                        break;
                    case TimerType.MinutesTimer:
                        time = "00:00";
                        break;
                    case TimerType.HoursTimer:
                        time = "00:00:00";
                        break;
                }
            }

            if (GUITimer != null)
            {
                GUITimer.text = time;
            }

            if (Timer != null)
            {
                Timer.text = time;
            }
        }
    }

    public void SetTimer(TimeSpan time)
    {
        TimeSpan = time.Add(new TimeSpan(0, 0, 1));

        string t = "";
        switch (TimerType)
        {
            case TimerType.SecondsTimer:
                t = time.TotalSeconds.ToString("00");
                break;
            case TimerType.MinutesTimer:
                t = (time.Days * 24 * 60 + time.Hours * 60 + time.Minutes).ToString("00") + ":" +
                       time.Seconds.ToString("00");
                break;
            case TimerType.HoursTimer:
                t = (time.Days * 24 + time.Hours).ToString("00") + ":" + time.Minutes.ToString("00") + ":" +
                       time.Seconds.ToString("00");
                break;
        }

        if (GUITimer != null)
        {
            GUITimer.text = t;
        }

        if (Timer != null)
        {
            Timer.text = t;
        }

    }

    public void StartTimer()
    {
        StartTime = DateTime.Now;
        TimerActive = true;

        OnStart();        
    }

    public void StartTimer(TimeSpan time)
    {
        StartTime = DateTime.Now;
        TimeSpan = time.Add(new TimeSpan(0, 0, 1));
        TimerActive = true;

        OnStart();
    }

    protected void OnStart()
    {
        if (Start != null)
        {
            Start();
        }
    }

    protected void OnStop()
    {
        if (Stop != null)
        {
            Stop();
        }
    }
}

public enum TimerType
{
    SecondsTimer,
    MinutesTimer,
    HoursTimer
}