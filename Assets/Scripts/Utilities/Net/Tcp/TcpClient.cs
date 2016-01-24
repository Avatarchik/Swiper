using System;
using System.Net;
using System.Net.Sockets;

public class TcpClient
{
    public const int ReceivedSize = 2048;

    public Action Accept;

    public Action Connect;

    public Action Send;

    public Action<TcpResponce> Receive;

    public Action Disconnect;

    protected IPEndPoint Ipep;

    protected Socket ServerSocket;

    protected Socket ClientSocket;

    protected byte[] ReceivedData;

    public void BeginAccept(int port)
    {
        Ipep = new IPEndPoint(IPAddress.Any, port);
        ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ServerSocket.Bind(Ipep);
        ServerSocket.Listen(1);
        ServerSocket.BeginAccept(OnAccept, ServerSocket);
    }

    public void OnAccept(IAsyncResult iar)
    {
        var socket = (Socket)iar.AsyncState;
        ClientSocket = socket.EndAccept(iar);
        if (Accept != null)
        {
            Accept();
        }
    }

    public void BeginConnect(string address, int port)
    {
        Ipep = new IPEndPoint(IPAddress.Parse(address), port);
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ClientSocket.BeginConnect(Ipep, OnConnect, ClientSocket);
    }

    protected void OnConnect(IAsyncResult iar)
    {
        var socket = (Socket)iar.AsyncState;
        socket.EndConnect(iar);
        if (Connect != null)
        {
            Connect();
        }
    }

    public void BeginSend(TcpMessage message)
    {
        byte[] sendData = message.Data;
        ClientSocket.BeginSend(sendData, 0, sendData.Length, 0, OnSend, ClientSocket);
    }

    protected void OnSend(IAsyncResult iar)
    {
        var socket = (Socket)iar.AsyncState;
        socket.EndSend(iar);

        if (Send != null)
        {
            Send();
        }
    }

    public void BeginReceive()
    {
        ReceivedData = new byte[ReceivedSize];
        ClientSocket.BeginReceive(ReceivedData, 0, ReceivedSize, 0, OnReceive, ClientSocket);
    }

    protected void OnReceive(IAsyncResult iar)
    {
        var socket = (Socket)iar.AsyncState;
        int length = socket.EndReceive(iar);

        TcpResponce tcpResponce = new TcpResponce(ReceivedData, length);

        if (Receive != null)
        {
            Receive(tcpResponce);
        }
    }

    public void BeginDisconnect()
    {
        ServerSocket.Close();
    }

    protected void OnDisconnect(IAsyncResult iar)
    {
        var socket = (Socket)iar.AsyncState;
        socket.EndDisconnect(iar);

        if (Disconnect != null)
        {
            Disconnect();
        }
    }
}
