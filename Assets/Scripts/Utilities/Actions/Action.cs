using System;
using System.Collections;

namespace AraxisTools
{
    public abstract class Action
    {
        public event Action<Action> Start;

        public event Action<Action> Finish;

        public event Action<Action> Update;

        public event Action<Action> Interrupt;

        public event Action<Action> TagChanged;

        public event Action<Action> NameChanged;

        public string Tag { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; protected set; }

        public bool IsInterrupted { get; protected set; }

        public virtual IEnumerable NextFrame()
        {
            OnStart();
            yield return null;
            OnFinish();
        }

        public virtual void InterruptEvent()
        {
            IsInterrupted = true;
            OnInterrupt();
        }

        protected virtual void OnStart()
        {
            IsActive = true;
            if (Start != null)
            {
                Start(this);
            }
        }

        protected virtual void OnFinish()
        {
            IsActive = false;
            if (Finish != null)
            {
                Finish(this);
            }
        }

        protected virtual void OnUpdate()
        {
            if (Update != null)
            {
                Update(this);
            }
        }

        protected virtual void OnInterrupt()
        {
            IsInterrupted = true;
            if (Interrupt != null)
            {
                Interrupt(this);
            }
        }

        protected virtual void OnTagChanged()
        {
            if (TagChanged != null)
            {
                TagChanged(this);
            }
        }

        protected virtual void OnNameChanged()
        {
            if (NameChanged != null)
            {
                NameChanged(this);
            }
        }

        public override string ToString()
        {
            return "Action = { Tag = [" + Tag + "] , Name = [" + Name + "] , IsActive = [" + IsActive +
                   "] , IsInterrupted = [" + IsInterrupted + "]";
        }
    }
}