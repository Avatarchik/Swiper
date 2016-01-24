using AraxisTools.Json;
using SimpleJSON;

public interface IJsonable
{
    JsonClass GetJson();

    JsonClass GetJson(string className);

    void ParseJson(JSONNode json);
}