using SimpleJSON;
using UnityEngine;

public static class JsonNodeExtension
{
    public static Vector3? GetVector3(this JSONNode json)
    {
        if (json != null)
        {
            var vector = new Vector3
            {
                x = json[0].AsFloat,
                y = json[1].AsFloat,
                z = json[2].AsFloat
            };
            return vector;
        }
        return null;
    }
}
