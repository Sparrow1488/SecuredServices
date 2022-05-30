namespace SecuredServices.Core.Messages
{
    public interface IProtectorMessage
    {
        public string Message { get; }
        public string MessageType { get; }
    }
}
