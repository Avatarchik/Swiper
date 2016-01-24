using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFromCache
{
    public const string JsonName = "name";

    public const string JsonPath = "path";

    public const string JsonDependencies = "dependencies";

    public string Name;

    public string Path;

    public List<string> Dependencies;

    public ObjectFromCache(string name, string path, List<string> dependencies = null)
    {
        Name = name;
        Path = path;
        Dependencies = dependencies;
    }
}
