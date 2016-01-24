using System.Collections;
using AraxisTools;

public class TcpNetworkAction : Action
{
    public event System.Action<Action, TcpResponce> ResponceReceived;

    public TcpMessage Message;

    public TcpResponce Responce;

    protected virtual void OnResponceReceived()
    {
        if (ResponceReceived != null)
        {
            ResponceReceived(this, Responce);
        }
    }

    public TcpNetworkAction(TcpMessage message)
    {
        Message = message;
    }

    public override IEnumerable NextFrame()
    {
        OnStart();

        TcpNetworkController.Instance.Send(Message);

        while (Message == null)
        {
            yield return null;
        }

        OnResponceReceived();

        OnFinish();
    }
}
