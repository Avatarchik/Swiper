using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup<T> : Singleton<T> where T : MonoBehaviour
{
    public Action<T> Show;

    public Action<T> Hide;

    public bool IsActive;

    public override void Destroy()
    {
        Instance = null;
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public virtual void ShowPopup()
    {
        OnShow();
    }

    public virtual void HidePopup()
    {
        OnHide();
    }

    protected void OnShow()
    {
        if (Show != null)
        {
            Show(Instance);
        }
    }

    protected void OnHide()
    {
        if (Hide != null)
        {
            Hide(Instance);
        }

        IsActive = false;
    }

    public virtual void OnButtonClick()
    {
        GameController.RestoreCursor();
    }

    public virtual void OnPointerEnter(BaseEventData eventData)
    {
        GameController.SetCursor();
    }

    public virtual void OnPointerExit(BaseEventData eventData)
    {
        GameController.RestoreCursor();
    }

}
