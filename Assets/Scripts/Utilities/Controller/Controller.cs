using UnityEngine;
using System.Collections;

public class Controller<T> : Singleton<T> where T : MonoBehaviour
{
    public TextAsset Config;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        Config = Resources.Load<TextAsset>("Configs/" + typeof(T) + "Config");
        ParseConfig();
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

    protected virtual void ParseConfig()
    {
        
    }
}
