namespace SecuredServices.Core.Exceptions
{
    public class AccessDeniedException : SecuredServiceException
    {
        public AccessDeniedException() { }
        public AccessDeniedException(string message) : base(message) { }
    }
}
