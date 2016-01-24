using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GUI<T> : Singleton<T> where T : MonoBehaviour
{
    public event Action Enable;

    public event Action Disable;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        AddHandlers();
        AddEvents();
        InitGUI();
    }

    public virtual void InitGUI()
    {
        
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
        RemoveEvents();
    }

    public virtual void EnableGUI()
    {

    }

    public virtual void DisableGUI()
    {

    }

    private void OnEnable()
    {
        if (Enable != null)
        {
            Enable();
        }
    }

    private void OnDisable()
    {
        if (Disable != null)
        {
            Disable();
        }
    }

    public virtual void AddEvents()
    {
        
    }

    public virtual void RemoveEvents()
    {
        
    }

    public virtual void AddHandlers()
    {
        
    }

    public virtual void RemoveHandlers()
    {
        
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
