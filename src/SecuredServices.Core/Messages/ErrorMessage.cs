namespace SecuredServices.Core.Messages
{
    public class ErrorMessage : IProtectorMessage
    {
        public ErrorMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
        public string MessageType => ProtectorMessageType.Error;
    }
}
