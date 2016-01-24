using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleTransform : TransformBase
{
    public Vector3 StartScale;

    public override void Start()
    {
        base.Start();
        StartScale = Transform.localScale;
    }

    public override void StartTween()
    {
        Tween = Transform.DOScale(Target, Time);
        AddEvents();
    }

    public override void Refresh()
    {
        base.Refresh();
        StartScale = gameObject.transform.localScale;
    }

    public override void Kill()
    {
        base.Kill();
        Transform.localScale = StartScale;
    }

}
