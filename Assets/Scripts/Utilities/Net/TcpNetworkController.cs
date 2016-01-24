using AraxisTools;
using SimpleJSON;
using UnityEngine;

public class TcpNetworkController : Controller<TcpNetworkController>
{
    public ActionsQueue TcpQueue;

    public string TcpIpAddress;

    public int TcpPort = 10666;

    public bool TcpHasConnection;

    private TcpClient _client;

    public override void Start()
    {
        base.Start();
        _client = new TcpClient();
        Accept();
        StartCoroutine(TcpQueue.ProcessQueue());
    }

    protected override void ParseConfig()
    {
        JSONNode node = JSONNode.Parse(Config.text);

        TcpIpAddress = node["network"]["ip_address"];
        TcpPort = node["network"]["port"].AsInt;
    }

    public void Accept()
    {
        Debug.Log("Begin Accept");
        _client.BeginAccept(TcpPort);
        _client.Accept += OnAccept;
        _client.Connect += OnConnect;
        _client.Send += OnSend;
        _client.Receive += OnReceive;
        _client.Disconnect += OnDisconnect;
    }

    public void OnAccept()
    {
        Debug.Log("On Accept");
        TcpHasConnection = true;
    }

    public void Connect()
    {
        Debug.Log("Begin Connect");
        _client.BeginConnect(TcpIpAddress, TcpPort);
    }

    public void OnConnect()
    {
        Debug.Log("On Connect");
        Receive();
    }

    public void Send(TcpMessage message)
    {
        if (Instance.TcpHasConnection)
        {
            Debug.Log("Begin Send");
            _client.BeginSend(message);
        }
    }

    public void OnSend()
    {
        Debug.Log("On Send");

    }

    public void Receive()
    {
        Debug.Log("Begin Receive");
        _client.BeginReceive();
    }

    public void OnReceive(TcpResponce message)
    {
        Debug.Log("On Receive");

        TcpNetworkAction tcpAction = TcpQueue.CurrentEvent as TcpNetworkAction;
        if (tcpAction != null)
        {
            tcpAction.Responce = message;
        }

        Receive();
    }

    public void Disconnect()
    {
        _client.BeginDisconnect();
    }

    public void OnDisconnect()
    {

    }
}
