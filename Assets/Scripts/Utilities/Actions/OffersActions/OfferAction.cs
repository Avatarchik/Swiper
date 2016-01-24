using UnityEngine;
using System.Collections;
using AraxisTools;

public class OfferAction<T> : Action where T : Offer<T>
{
    public override IEnumerable NextFrame()
    {
        OnStart();

        while (Offer<T>.Instance.IsActive)
        {
            yield return null;
        }

        OnFinish();
    }
}
