
namespace JDS.Messenger
{
    public interface IMessageReceiver
    {
        void ReceiveMessage(MessageHandler message);
    }
}
