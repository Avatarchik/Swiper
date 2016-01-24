using UnityEngine;
using System.Collections;
using System.IO;

public static class Writer
{
    public static void Write(object obj, string path)
    {
        var sw = new StreamWriter(path);
        sw.Write(obj.ToString());
        sw.Close();
    }
}
