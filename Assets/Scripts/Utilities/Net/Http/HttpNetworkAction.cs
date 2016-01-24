using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Action = AraxisTools.Action;

public class HttpNetworkAction : Action
{
    public event System.Action<Action, HttpResponce> ResponceReceived;

    public string RequestName;

    public string PostData;

    private WWW _request;

    private HttpResponce _responce;

    protected virtual void OnResponceReceived()
    {
        if (ResponceReceived != null)
        {
            ResponceReceived(this, _responce);
        }
    }

    public HttpNetworkAction(HttpMessage message)
    {
        RequestName = HttpNetworkController.Instance.HttpAddress + message.Request;
        PostData = "";//= "version=" + Honey.NetworkManager.ServerVersion + "&id=" + PlayerManager.Id;

        string additionalParameters = message.Parameters.Aggregate("", (current, par) => current + ("&" + par.Key + "=" + par.Value.ToLower()));

        PostData += additionalParameters;
    }

    public override IEnumerable NextFrame()
    {
        OnStart();
        byte[] bytes = Encoding.UTF8.GetBytes(PostData);

        //Debug.Log(RequestName);
        //Debug.Log(PostData);

        _request = new WWW(RequestName, bytes, HttpNetworkController.Instance.HttpHeaders);

        while (!_request.isDone)
        {
            yield return null;
        }

        if (String.IsNullOrEmpty(_request.error))
        {
            Debug.Log(_request.text);
            _responce = new HttpResponce(_request.text);
            OnResponceReceived();
        }
        else
        {
            Debug.LogError("Error on request:  " + RequestName + " => " + _request.error);
        }
        OnFinish();
    }
}
