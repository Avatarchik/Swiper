using UnityEngine;
using System.Collections;

public class UnityPoolObject : MonoBehaviour, IPoolObject<string>
{
    public virtual string Group
    {
        get
        {
            return name;
        }
    }

    public Transform Transform
    {
        get
        {
            return _transform;
        }
    }

    protected Transform _transform;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    public virtual void SetTransform(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public virtual void Create() 
    {
        gameObject.SetActive(true);
    }

    public virtual void OnPush() 
    {
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        var n = gameObject.name.Replace("(Clone)", string.Empty);
        n = n.Replace(" ", string.Empty);
        UnityPoolController.Instance.Push(n, this);
    }

    public void FailedPush() 
    {
        Debug.Log("FailedPush");
        Destroy(gameObject);
    }
}