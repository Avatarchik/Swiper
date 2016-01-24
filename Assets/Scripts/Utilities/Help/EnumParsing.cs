using System;
using UnityEngine;
using System.Collections;

public static class EnumParsing
{
    public static T TryParse<T>(string s)
    {
        //Debug.Log("string = " + s);
        return (T)Enum.Parse(typeof(T), s.Replace("\"", ""));
    }
}
