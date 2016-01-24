
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AraxisTools
{
    public class EndlessTimerAction : Action
    {
        private readonly Dictionary<float, Action<object>> _timerActions = new Dictionary<float, Action<object>>();

        public float Timer { get; private set; }

        public override IEnumerable NextFrame()
        {
            IsActive = true;
            OnStart();
            while (!IsInterrupted)
            {
                Timer += Time.deltaTime;
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