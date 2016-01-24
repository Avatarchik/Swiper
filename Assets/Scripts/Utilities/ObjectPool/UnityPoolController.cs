using AraxisTools;
using UnityEngine;

public class UnityPoolController : Controller<UnityPoolController>
{
    public int MaxInstanceCount = 128;

    public int CurrentCount;

    public PoolController<string, UnityPoolObject> PoolManager;

    private ActionsQueue _loadQueue;

    public override void Start()
    {
        base.Start();
        _loadQueue = new ActionsQueue();
        StartCoroutine(_loadQueue.ProcessQueue());
    }

    public override void Awake()
    {
        base.Awake();
        if (gameObject != null)
        {
            PoolManager = new PoolController<string, UnityPoolObject>(MaxInstanceCount);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
#if UNITY_EDITOR
        CurrentCount = PoolManager.InctanceCount;
#endif
    }

    protected override void ParseConfig()
    {
        
    }

    public virtual bool CanPush()
    {
        return PoolManager.CanPush();
    }

    public virtual bool Push(string groupKey, UnityPoolObject poolObject)
    {
        if (PoolManager.Push(groupKey, poolObject))
        {
            poolObject.transform.SetParent(gameObject.transform);
            return true;
        }
        else
        {
            Destroy(poolObject.gameObject);
            return false;   
        }
    }

    public virtual T PopOrCreate<T>(T prefab) where T : UnityPoolObject
    {
        return PopOrCreate(prefab, Vector3.zero, Quaternion.identity);
    }

    public virtual T PopOrCreate<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {
        T result = PoolManager.Pop<T>(prefab.Group);
        if (result == null)
        {
            result = CreateObject<T>(prefab, position, rotation);
        }
        else
        {
            result.SetTransform(position, rotation);
        }
        return result;
    }

    public virtual UnityPoolObject Pop(string groupKey)
    {
        return PoolManager.Pop<UnityPoolObject>(groupKey);
    }

    public virtual T Pop<T>() where T : UnityPoolObject
    {
        return PoolManager.Pop<T>();
    }

    public virtual T Pop<T>(PoolController<string, UnityPoolObject>.Compare<T> comparer) where T : UnityPoolObject
    {
        return PoolManager.Pop<T>(comparer);
    }

    public virtual T Pop<T>(string groupKey) where T : UnityPoolObject
    {
        return PoolManager.Pop<T>(groupKey);
    }

    public virtual bool Contains(string groupKey)
    {
        return PoolManager.Contains(groupKey);
    }

    public virtual void Clear()
    {
        PoolManager.Clear();
    }

    protected virtual T CreateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {
        GameObject go = Instantiate(prefab.gameObject, position, rotation) as GameObject;
        T result = go.GetComponent<T>();
        result.name = prefab.name;
        return result;
    }
}