using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace JDS
{
    public class Messenger
    {
        public static Messenger Get = new Messenger();
        
        private List<MessageReceiverElement> _messageReceivers = new List<MessageReceiverElement>();
        private List<MessageHandler> _sureMessages = new List<MessageHandler>();

        public Messenger Add(IMessageReceiver messageReceiver)
        {
            _messageReceivers.Add(new MessageReceiverElement(messageReceiver));
            return this;
        }
        
        public void SendMessage(string message)
        {
            foreach (var messageReceiver in _messageReceivers)
            {
                messageReceiver.TrySendMessage(new MessageHandler(message, true));
            }
        }

        public void EnableReceiver(IMessageReceiver receiver)
        {
            int index = _messageReceivers.FindIndex(x => x.Equals(receiver));
            if (index <= -1)
            {
                DebugLog.LogWarning("Cant find receiver to enable");
                return;
            }
            _messageReceivers[index].IsActive = true;
            SendSureMessages();
        }

        public void DisableReceiver(IMessageReceiver receiver)
        {
            int index = _messageReceivers.FindIndex(x => x.Equals(receiver));
            if (index <= -1)
            {
                DebugLog.LogWarning("Cant find receiver to disable");
                return;
            }
            _messageReceivers[index].IsActive = false;
        }

        public void SendSureMessage(string message)
        {
            if (_sureMessages.Any(x => x.Equals(message)))
            {
                DebugLog.LogWarning($"Sure messages already contains message: {message}");    
                return;
            }
            
            _sureMessages.Add(new MessageHandler(message));
            SendSureMessages();
        }

        private void SendSureMessages()
        {
            bool isDirty = false;
            
            foreach (var receiverElement in _messageReceivers)
            {
                foreach (var messageHandler in _sureMessages)
                {
                    if (!receiverElement.TrySendMessage(messageHandler)) continue;
                    
                    if (messageHandler.IsReceived)
                    {
                        isDirty = true;
                        break;
                    }
                }
            }
            
            if(isDirty)
                ClearReceivedMessages();
        }

        private void ClearReceivedMessages()
        {
            _sureMessages.RemoveAll(x => x.IsReceived);
        }

        private class MessageReceiverElement
        {
            public bool IsActive;
            
            private IMessageReceiver _messageReceiver;
            public MessageReceiverElement(IMessageReceiver messageReceiver, bool isActive = false)
            {
                _messageReceiver = messageReceiver;
                IsActive = isActive;
            }

            public bool TrySendMessage(MessageHandler message)
            {
                if (!IsActive)
                    return false;
                _messageReceiver.ReceiveMessage(message);
                return true;
            }

            public bool Equals(IMessageReceiver messageReceiver)
            {
                return _messageReceiver == messageReceiver;
            }
        }
    }
    
    public class MessageHandler
    {
        private string _message;
        private bool _isReceived;

        public MessageHandler(string message, bool isReceived = false)
        {
            _message = message;
            _isReceived = isReceived;
        }

        public string Message => _message;
        public void Received() => _isReceived = true;
        public bool IsReceived => _isReceived;

        public bool Equals(string message)
        {
            return _message == message;
        }
    }

    public interface IMessageReceiver
    {
        void ReceiveMessage(MessageHandler message);
    }
}