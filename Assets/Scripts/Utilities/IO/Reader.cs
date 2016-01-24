using System.IO;
using SimpleJSON;

public static class Reader
{
    public static string Read(string path)
    {
        var reader = new StreamReader(path);
        var s = reader.ReadToEnd();
        reader.Close();
        return s;
    }

    public static JSONNode ReadJson(string path)
    {
        var reader = new StreamReader(path);
        var s = reader.ReadToEnd();
        reader.Close();
        return JSONNode.Parse(s);
    }
}
