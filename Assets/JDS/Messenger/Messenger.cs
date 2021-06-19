using System.Collections.Generic;
using System.Linq;

namespace JDS.Messenger
{
    public class Messenger
    {
        public static readonly Messenger Get = new Messenger();
        
        private readonly List<MessageReceiverElement> _messageReceivers = new List<MessageReceiverElement>();
        private readonly List<MessageHandler> _sureMessages = new List<MessageHandler>();

        public Messenger Add(IMessageReceiver messageReceiver)
        {
            _messageReceivers.Add(new MessageReceiverElement(messageReceiver));
            return this;
        }
        
        public void SendMessage(string message)
        {
            foreach (var messageReceiver in _messageReceivers)
            {
                if(messageReceiver.TrySendMessage(new MessageHandler(message, true)))
                    return;
            }
        }

        public void EnableReceiver(IMessageReceiver receiver)
        {
            if (TryFindReceiverIndex(receiver, out int index))
            {
                _messageReceivers[index].IsActive = true;
                SendSureMessages();
            }
        }

        public void DisableReceiver(IMessageReceiver receiver)
        {
            if (TryFindReceiverIndex(receiver, out int index))
            {
                _messageReceivers[index].IsActive = false;
            }
        }

        public bool TryFindReceiverIndex(IMessageReceiver receiver, out int index)
        {
            index = _messageReceivers.FindIndex(x => x.Equals(receiver));
            if (index <= -1)
            {
                DebugLog.LogWarning("Cant find receiver to disable");
                return false;
            }
            return true;
        }

        public void SendSureMessage(string message)
        {
            if (_sureMessages.Any(x => x.Equals(message)))
            {
                DebugLog.LogWarning($"Sure messages already contains message: {message}", "REMAINING SURE MESSAGES");    
                return;
            }
            
            _sureMessages.Add(new MessageHandler(message));
            SendSureMessages();
        }

        private void SendSureMessages()
        {
            if(_sureMessages.Count <= 0 || _messageReceivers.Count <= 0)
                return;
            
            bool isDirty = false;
            
            foreach (var receiverElement in _messageReceivers)
            {
                foreach (var messageHandler in _sureMessages)
                {
                    if (!receiverElement.TrySendMessage(messageHandler)) continue;
                    
                    if (messageHandler.IsReceived.Value)
                    {
                        isDirty = true;
                        break;
                    }
                }
            }
            
            if(isDirty)
                ClearReceivedMessages();
            
            if(_sureMessages.Count > 0)
                DebugLog.LogWarning(GetLogSureMessages(), "REMAINING SURE MESSAGES");
        }

        private string GetLogSureMessages()
        {
            string log = "";
            foreach (var messageHandler in _sureMessages)
            {
                log += messageHandler.Message + ", ";
            }

            return log;
        }

        private void ClearReceivedMessages()
        {
            _sureMessages.RemoveAll(x => x.IsReceived.Value);
        }

        private class MessageReceiverElement
        {
            public bool IsActive;
            
            private readonly IMessageReceiver _messageReceiver;
            public MessageReceiverElement(IMessageReceiver messageReceiver, bool isActive = false)
            {
                _messageReceiver = messageReceiver;
                IsActive = isActive;
            }

            public bool TrySendMessage(MessageHandler message)
            {
                if (!IsActive)
                    return false;
                
                DebugLog.Log(message.Message, "MESSAGE SEND");
                
                _messageReceiver.ReceiveMessage(message);
                return true;
            }

            public bool Equals(IMessageReceiver messageReceiver)
            {
                return _messageReceiver == messageReceiver;
            }
        }
    }
}
