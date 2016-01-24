using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    public SingletonType SingletonType;

    protected Singleton()
    {
        
    }

    public virtual void Destroy()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
            if (SingletonType == SingletonType.Indestructible)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void OnDestroy()
    {
        
    }
}

public enum SingletonType
{
    DestroyOnLoad,
    Indestructible
}