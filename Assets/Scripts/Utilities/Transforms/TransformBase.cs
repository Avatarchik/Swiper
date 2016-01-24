using System;
using UnityEngine;
using System.Collections;
using AraxisTools.Json;
using DG.Tweening;
using SimpleJSON;
using UnityEngine.Events;

public class TransformBase : MonoBehaviour, IJsonable
{
    public TransformType Type;

    public bool PlayOnAwake;

    public bool Test;

    public bool IsLocal = true;

    public bool IsDependent = true;

    public Vector3 Target;

    public float Time;

    public Ease Ease;

    public bool IsLoop = true;

    public int Loops = -1;

    public LoopType LoopType;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent StartEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent PlayEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent StepCompleteEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent UpdateEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent PauseEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent KillEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent CompleteEvent;

#if HIDE_TRUE
    [HideInInspector]
#endif
    public UnityEvent RewindEvent;

    protected Transform Transform;

    protected Tween Tween;

    public virtual void Awake()
    {
        Transform = gameObject.transform;
        if (PlayOnAwake)
        {
            Debug.LogError("Play on Awake");
            StartTween();
        }
    }

    public virtual void Start()
    {
        //Debug.LogError("START BASE");
    }

    public virtual void StartTween()
    {
        //Debug.LogError("START TWEEN BASE");
    }

    protected virtual void AddEvents()
    {
        //Debug.LogError("ADD EVENTS BASE");

        Tween.SetEase(Ease);

        if (IsLoop)
        {
            Tween.SetLoops(Loops, LoopType);
        }

        if (StartEvent == null)
        {
            StartEvent = new UnityEvent();
        }
        Tween.OnStart(() => { StartEvent.Invoke(); });

        if (PlayEvent == null)
        {
            PlayEvent = new UnityEvent();
        }
        Tween.OnPlay(() => { PlayEvent.Invoke(); });

        if (StepCompleteEvent == null)
        {
            StepCompleteEvent = new UnityEvent();
        }
        Tween.OnStepComplete(() => { StepCompleteEvent.Invoke(); });

        if (UpdateEvent == null)
        {
            UpdateEvent = new UnityEvent();
        }
        Tween.OnUpdate(() => { UpdateEvent.Invoke(); });

        if (PauseEvent == null)
        {
            PauseEvent = new UnityEvent();
        }
        Tween.OnPause(() => { PauseEvent.Invoke(); });

        if (KillEvent == null)
        {
            KillEvent = new UnityEvent();
        }
        Tween.OnKill(() => { KillEvent.Invoke(); });

        if (CompleteEvent == null)
        {
            CompleteEvent = new UnityEvent();
        }
        Tween.OnComplete(() => { CompleteEvent.Invoke(); });

        if (RewindEvent == null)
        {
            RewindEvent = new UnityEvent();
        }
        Tween.OnRewind(() => { RewindEvent.Invoke(); });
    }

    public virtual void Refresh()
    {
        //Debug.LogError("REFRESH BASE");
    }

    public virtual void Play()
    {
        //Debug.LogError("PLAY BASE");
        Tween.Play();
    }

    public virtual void Pause()
    {
        //Debug.LogError("PAUSE BASE");
        Tween.Pause();
    }

    public virtual void Kill()
    {
        //Debug.LogError("KILL BASE");
        Tween.Kill();
    }

    public virtual void Restart()
    {
        //Debug.LogError("RESTART BASE");
        Tween.Restart();
    }

    public virtual JsonClass GetJson()
    {
        var json = new JsonClass();

        json.AddObjects(new JsonValue("type", Type));
        json.AddObjects(new JsonValue("play_on_awake", PlayOnAwake));
        json.AddObjects(new JsonValue("is_local", IsLocal));
        json.AddObjects(new JsonValue("is_dependent", IsDependent));
        json.AddObjects(new JsonArray("target", Target));
        json.AddObjects(new JsonValue("time", Time));
        json.AddObjects(new JsonValue("ease", Ease));
        json.AddObjects(new JsonValue("is_loop", IsLoop));
        json.AddObjects(new JsonValue("loops", Loops));
        json.AddObjects(new JsonValue("loop_type", LoopType));

        return json;
    }

    public virtual JsonClass GetJson(string className)
    {
        var json = GetJson();
        json.SetKey(className);
        return json;
    }

    public virtual void ParseJson(JSONNode json)
    {
        //Debug.LogError("PARSE BASE");
        Type = EnumParsing.TryParse<TransformType>(json["type"]);
        PlayOnAwake = json["play_on_awake"].AsBool;
        IsLocal = json["is_local"].AsBool;
        IsDependent = json["is_dependent"].AsBool;
        var vector3 = json["target"].GetVector3();
        if (vector3 != null)
        {
            Target = (Vector3) vector3;
        }
        Time = json["time"].AsFloat;
        Ease = EnumParsing.TryParse<Ease>(json["ease"]);
        IsLoop = json["is_loop"].AsBool;
        Loops = json["loops"].AsInt;
        LoopType = EnumParsing.TryParse<LoopType>(json["loop_type"]);
    }
}

public enum TransformType
{
    Moving, Rotating, Scaling
}