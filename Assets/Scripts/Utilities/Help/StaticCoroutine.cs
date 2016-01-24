using System.Collections;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    public static StaticCoroutine CreateStaticCoroutine(string name = "StaticCoroutine")
    {
        var coroutine = new GameObject();
        DontDestroyOnLoad(coroutine);
        coroutine.name = name;
        coroutine.AddComponent<StaticCoroutine>();
        return coroutine.GetComponent<StaticCoroutine>();
    }

    public Coroutine StartStaticCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
}