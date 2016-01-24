using DG.Tweening;
using UnityEngine;

public class MoveTransform : TransformBase
{
    public Vector3 StartMove;

    public override void Start()
    {
        //Debug.LogError("START");
        base.Start();
        //Debug.LogError("Transform = " + Transform.localPosition);
        StartMove = Transform.localPosition;
        //Debug.LogError("Transform = " + Transform.localPosition);
    }

    public override void StartTween()
    {
        //Debug.LogError("START TWEEN");
        if (IsDependent)
        {
            if (IsLocal)
            {
                Tween = Transform.DOLocalMove(Transform.localPosition + Target, Time);
            }
            else
            {
                Tween = Transform.DOMove(Transform.position + Target, Time);
            }
        }
        else
        {
            if (IsLocal)
            {
                Tween = Transform.DOLocalMove(Target, Time);
            }
            else
            {
                Tween = Transform.DOMove(Target, Time);
            }
        }

        AddEvents();
    }

    public override void Refresh()
    {
        //Debug.LogError("REFRESH");
        base.Refresh();
        StartMove = Transform.localPosition;
    }

    public override void Kill()
    {
        //Debug.LogError("KILL");
        base.Kill();
    }

}
