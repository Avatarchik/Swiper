using System;
using DG.Tweening;
using UnityEngine;

public class Swiper : Singleton<Swiper>, ICollisiable, ITriggerable
{
    public Transform Transform;

    public int Position = 2;

    private readonly float[] _positions = { -4.0f, -2.0f, 0.0f, 2.0f, 4.0f };

    #region ICollisiable implementation

    public void OnCollisionEnter2D(Collision2D obj)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionExit2D(Collision2D obj)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionStay2D(Collision2D obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region ITriggerable implementation

    public void OnTriggerEnter2D(Collider2D obj)
    {
        throw new NotImplementedException();
    }

    public void OnTriggerExit2D(Collider2D obj)
    {
        throw new NotImplementedException();
    }

    public void OnTriggerStay2D(Collider2D obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    public override void Awake()
    {
        base.Awake();
        Transform = gameObject.GetComponent<Transform>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var position = MainCameraController.Instance.Transform.position;
        position.y = Transform.position.y + 3.6f;
        MainCameraController.Instance.Transform.position = position;
    }

    public void ShiftLeft()
    {
        if (Position > 0)
        {
            var tween = transform.DOLocalMoveX(_positions[Position - 1], 0.2f);
            tween.SetEase(Ease.OutCubic);
            Position--;
        }
    }

    public void ShiftRight()
    {
        if (Position < _positions.Length - 1)
        {
            var tween = transform.DOLocalMoveX(_positions[Position + 1], 0.2f);
            tween.SetEase(Ease.OutCubic);
            Position++;
        }
    }
}
