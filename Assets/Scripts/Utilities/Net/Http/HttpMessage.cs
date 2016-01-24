using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpMessage
{
    public string Request;

    public KeyValuePair<string, string>[] Parameters;

    public HttpMessage(string request, KeyValuePair<string, string>[] parameters)
    {
        Request = request;
        Parameters = parameters;
    }
}
