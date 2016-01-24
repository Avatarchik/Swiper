using System.Collections.Generic;
using System;

public sealed class PoolController<TK, TV> where TV : IPoolObject<TK>
{
    public int MaxInstances;

    public int InctanceCount { get { return _objects.Count; } }

    public int CacheCount { get { return _cache.Count; } }

    public delegate bool Compare<in T>(T value) where T : TV;

    private readonly Dictionary<TK, List<TV>> _objects;

    private readonly Dictionary<Type, List<TV>> _cache;

    public PoolController(int maxInstance)
    {
        MaxInstances = maxInstance;
        _objects = new Dictionary<TK, List<TV>>();
        _cache = new Dictionary<Type, List<TV>>();
    }

    public bool CanPush()
    {
        return InctanceCount + 1 < MaxInstances;
    }

    public bool Push(TK groupKey, TV value)
    {
        if (CanPush())
        {
            value.OnPush();
            if (!_objects.ContainsKey(groupKey))
            {
                _objects.Add(groupKey, new List<TV>());
            }
            _objects[groupKey].Add(value);
            Type type = value.GetType();
            if (!_cache.ContainsKey(type))
            {
                _cache.Add(type, new List<TV>());
            }
            _cache[type].Add(value);
            return true;
        }
        else
        {
            value.FailedPush();
            return false;
        }
    }

    public T Pop<T>(TK groupKey) where T : TV
    {
        T result = default(T);
        if (Contains(groupKey) && _objects[groupKey].Count > 0)
        {
            for (int i = 0; i < _objects[groupKey].Count; i++)
            {
                if (_objects[groupKey][i] is T)
                {
                    result = (T)_objects[groupKey][i];
                    Type type = result.GetType();
                    RemoveObject(groupKey, i);
                    RemoveFromCache(result, type);
                    result.Create();
                    break;
                }
            }
        }
        return result;
    }

    public T Pop<T>() where T : TV
    {
        T result = default(T);
        Type type = typeof(T);
        if (ValidateForPop(type))
        {
            for (int i = 0; i < _cache[type].Count; i++)
            {
                result = (T)_cache[type][i];
                if (result != null && _objects.ContainsKey(result.Group))
                {
                    _objects[result.Group].Remove(result);
                    RemoveFromCache(result, type);
                    result.Create();
                    break;
                }

            }
        }
        return result;
    }

    public T Pop<T>(Compare<T> comparer) where T : TV
    {
        T result = default(T);
        Type type = typeof(T);
        if (ValidateForPop(type))
        {
            for (int i = 0; i < _cache[type].Count; i++)
            {
                T value = (T)_cache[type][i];
                if (comparer(value))
                {
                    _objects[value.Group].Remove(value);
                    RemoveFromCache(result, type);
                    result = value;
                    result.Create();
                    break;
                }

            }
        }
        return result;
    }

    public bool Contains(TK groupKey)
    {
        return _objects.ContainsKey(groupKey);
    }

    public void Clear()
    {
        _objects.Clear();
    }

    private bool ValidateForPop(Type type)
    {
        return _cache.ContainsKey(type) && _cache[type].Count > 0;
    }

    private void RemoveObject(TK groupKey, int idx)
    {
        if (idx >= 0 && idx < _objects[groupKey].Count)
        {
            _objects[groupKey].RemoveAt(idx);
            if (_objects[groupKey].Count == 0)
            {
                _objects.Remove(groupKey);
            }
        }
    }

    private void RemoveFromCache(TV value, Type type)
    {
        if (_cache.ContainsKey(type))
        {
            _cache[type].Remove(value);
            if (_cache[type].Count == 0)
            {
                _cache.Remove(type);
            }
        }
    }
}