using System.Collections;

namespace AraxisTools
{
    public class EmptyAction : Action
    {
        public EmptyAction(string tag = "empty", string name = "empty action")
        {
            Tag = tag;
            Name = name;
        }

        public override IEnumerable NextFrame()
        {
            IsActive = true;
            OnStart();
            OnUpdate();
            yield return null;
            if (!IsInterrupted)
            {
                OnUpdate();
                OnFinish();
            }
            IsActive = false;
        }
    }
}