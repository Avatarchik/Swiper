using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AraxisTools
{
    public class ActionsQueue
    {
        public static ActionsQueue Instance;

        public static void CreateStaticQueue(string queueName = "Default queue", string ownerName = "ActionsQueueOwner", bool isIndestructible = false)
        {
            var queueOwner = new GameObject { name = ownerName };
            queueOwner.AddComponent<ActionsQueueOwner>();
            var owner = queueOwner.GetComponent<ActionsQueueOwner>();
            owner.QueueName = queueName;
            owner.IsIndestructible = isIndestructible;
        }

        public Action CurrentEvent = new EmptyAction();

        public bool IsStatic { get; private set; }

        public string Name { get; set; }

        public bool IsTerminated { get; private set; }

        private readonly List<Action> _events = new List<Action>();

        public int Size
        {
            get { return _events.Count; }
        }

        public bool IsEmpty
        {
            get { return _events.Count == 0; }
        }

        public ActionsQueue(string name = "Default queue", bool isStatic = false)
        {
            IsStatic = isStatic;
            Name = name;
            if (isStatic)
            {
                Instance = this;
            }
        }

        public void AddAction(Action action)
        {
            _events.Add(action);
        }

        public bool RemoveAction(Action action)
        {
            return _events.Remove(action);
        }

        public bool RemoveAction(int index)
        {
            if (index > 0 && index < _events.Count)
            {
                _events.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAllTagged(string tag)
        {
            _events.RemoveAll(t => t.Tag == tag);
        }

        public void RemoveAllNamed(string name)
        {
            _events.RemoveAll(t => t.Name == name);
        }

        public void Clear()
        {
            _events.Clear();
            CurrentEvent.InterruptEvent();
            CurrentEvent = null;
        }

        public IEnumerator ProcessQueue()
        {
            var actionEnumerator = StartNextAction();

            while (true)
            {
                if (!actionEnumerator.MoveNext())
                {
                    actionEnumerator = StartNextAction();
                }

                yield return null;
            }
        }

        protected IEnumerator StartNextAction()
        {
            if (_events.Count > 0)
            {
                CurrentEvent = _events[0];
                _events.RemoveAt(0);
            }
            else
            {
                CurrentEvent = new EmptyAction();
            }

            return CurrentEvent.NextFrame().GetEnumerator();
        }
    }
}