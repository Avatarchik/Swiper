using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AraxisTools
{
    public class TimerAction : Action
    {
        private readonly Dictionary<float, Action<object>> _timerActions = new Dictionary<float, Action<object>>();

        public float Timer { get; private set; }

        public float Time { get; private set; }

        public float TimerProgress
        {
            get
            {
                return Timer / Time;
            }
        }

        public override IEnumerable NextFrame()
        {
            IsActive = true;
            OnStart();
            while (Timer < Time && !IsInterrupted)
            {
                Timer += UnityEngine.Time.deltaTime;
                OnUpdate();
                ProcessActions();
                yield return null;
            }

            if (!IsInterrupted)
            {
                OnUpdate();
                ProcessActions();
                OnFinish();
            }

            IsActive = false;
        }

        public TimerAction(float time)
        {
            Time = time;
        }

        private void ProcessActions()
        {
            foreach (var action in _timerActions.Where(action => action.Key <= Timer).Where(action => action.Value != null))
            {
                action.Value(this);
            }

            var toRemove = _timerActions.Where(action => action.Key <= Timer).ToList();

            foreach (var keyValuePair in toRemove)
            {
                _timerActions.Remove(keyValuePair.Key);
            }
        }

        public void AddAction(float time, Action<object> action, bool startRelative = true)
        {
            float keyTime;
            if (startRelative)
            {
                keyTime = time;
            }
            else
            {
                keyTime = Timer + time;
            }
            _timerActions.Add(keyTime, action);
        }
    }
}