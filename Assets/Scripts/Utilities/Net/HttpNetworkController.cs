using System.Collections.Generic;
using AraxisTools;
using SimpleJSON;
using UnityEngine;

public class HttpNetworkController : Controller<HttpNetworkController>
{
    public ActionsQueue NetworkQueue;

    public string HttpAddress;

    public Dictionary<string, string> HttpHeaders;

    public override void Start()
    {
        base.Start();
        NetworkQueue = new ActionsQueue();
        StartCoroutine(NetworkQueue.ProcessQueue());
    }

    protected override void ParseConfig()
    {
        JSONNode node = JSONNode.Parse(Config.text);

        HttpAddress = node["network"]["address"];
        HttpHeaders = new Dictionary<string, string>();
        int headersCount = node["network"]["headers"].AsArray.Count;
        for (int i = 0; i < headersCount; i++)
        {
            HttpHeaders.Add(node["network"]["headers"][i][0], node["network"]["headers"][i][1]);
        }
    }
}