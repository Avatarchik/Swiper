using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotateTransform : TransformBase
{
    public RotateMode RotateMode;

    public Vector3 StartAngle;

    public override void Start()
    {
        base.Start();
        StartAngle = Transform.localEulerAngles;
    }

    public override void StartTween()
    {
        if (IsDependent)
        {
            if (IsLocal)
            {
                Tween = Transform.DOLocalRotate(Transform.localEulerAngles + Target, Time, RotateMode);
            }
            else
            {
                Tween = Transform.DORotate(Transform.eulerAngles + Target, Time, RotateMode);
            }
        }
        else
        {
            if (IsLocal)
            {
                Tween = Transform.DOLocalRotate(Target, Time, RotateMode);
            }
            else
            {
                Tween = Transform.DORotate(Target, Time, RotateMode);
            }
        }

        AddEvents();
    }

    public override void Refresh()
    {
        base.Refresh();
        StartAngle = gameObject.transform.localEulerAngles;
    }

    public override void Kill()
    {
        base.Kill();
        Transform.localEulerAngles = StartAngle;
    }
}
