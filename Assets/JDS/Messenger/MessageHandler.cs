namespace JDS.Messenger
{
    public readonly struct MessageHandler
    {
        public string Message { get; }
        public Wrapper<bool> IsReceived {get;}
        
        public MessageHandler(string message, bool isReceived = false)
        {
            Message = message;
            IsReceived = new Wrapper<bool>(isReceived);
        }
        public void Received() => IsReceived.Value = true;
        public bool Equals(string message) => Message == message;
    }
}