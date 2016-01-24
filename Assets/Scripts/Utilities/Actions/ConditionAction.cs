using System.Collections;

namespace AraxisTools
{
    public class ConditionAction : Action
    {
        public override IEnumerable NextFrame()
        {
            IsActive = true;
            OnStart();
            while (!IsInterrupted)
            {
                OnUpdate();
                yield return null;
            }
            OnUpdate();
            IsActive = false;
            OnFinish();
        }
    }
}
