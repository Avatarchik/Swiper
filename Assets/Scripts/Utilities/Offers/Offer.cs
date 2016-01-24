using UnityEngine;
using System.Collections;

public class Offer<T> : Popup<T> where T : MonoBehaviour
{
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

    public override void ShowPopup()
    {
        base.ShowPopup();
    }

    public override void HidePopup()
    {
        base.ShowPopup();
    }
}
