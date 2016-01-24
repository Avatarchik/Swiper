using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;

public class ListObjectsFromCache
{
    private readonly List<ObjectFromCache> _objects = new List<ObjectFromCache>();

    private GameObject _currentObject;

    private List<Object> _assets = new List<Object>();

    public ListObjectsFromCache(TextAsset config)
    {
        var json = JSONNode.Parse(config.text);

        for (int i = 0; i < json.Childs.Count(); i++)
        {
            string name = json[i.ToString()][ObjectFromCache.JsonName];
            string path = json[i.ToString()][ObjectFromCache.JsonPath];
            var deps = new List<string>();
            for (var j = 0; j < json[i.ToString()][ObjectFromCache.JsonDependencies].AsArray.Count; j++)
            {
                deps.Add(json[i.ToString()][ObjectFromCache.JsonDependencies][j]);
            }
            _objects.Add(new ObjectFromCache(name, path, deps));
        }
    }

    public GameObject CreateObject(string name)
    {
        var obj = _objects.Find(cache => cache.Name == name);
        if (obj != null)
        {
            foreach (var d in obj.Dependencies)
            {
                _assets.Add(Resources.Load(d));
            }

            var prefab = Resources.Load(obj.Path);
            _currentObject = Object.Instantiate(prefab) as GameObject;
        }
        else
        {
            Debug.LogError("Object '" + name + "' missed! Check your config files" );
        }
        return _currentObject;
    }

    public void DestroyObject()
    {
        Object.Destroy(_currentObject);
        _currentObject = null;
        foreach (var asset in _assets)
        {
            Resources.UnloadAsset(asset);
        }
    }
}
