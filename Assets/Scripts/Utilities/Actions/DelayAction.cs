using System.Collections;
using System.Diagnostics;
using AraxisTools;

public class DelayAction : Action
{
    private readonly float _delay;

    public float Timer { get; private set; }

    public float DelayProgress
    {
        get
        {
            return Timer/_delay;
        }
    }

    public float Time
    {
        get
        {
            return Timer;
        }
    }

    public DelayAction(float delay)
    {
        _delay = delay;
        Timer = 0;
    }

    public override IEnumerable NextFrame()
    {
        IsActive = true;
        OnStart();
        while (Timer < _delay && !IsInterrupted)
        {
            Timer += UnityEngine.Time.deltaTime;
            OnUpdate();
            yield return null;
        }

        if (!IsInterrupted)
        {
            OnUpdate();
            OnFinish();
        }
        IsActive = false;
    }
}
