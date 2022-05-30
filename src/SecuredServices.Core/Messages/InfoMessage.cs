namespace SecuredServices.Core.Messages
{
    internal class InfoMessage : IProtectorMessage
    {
        public InfoMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
        public string MessageType => ProtectorMessageType.Info;
    }
}
