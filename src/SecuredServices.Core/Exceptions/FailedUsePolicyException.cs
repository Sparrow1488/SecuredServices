namespace SecuredServices.Core.Exceptions
{
    internal class FailedUsePolicyException : SecuredServiceException
    {
        public FailedUsePolicyException() { }
        public FailedUsePolicyException(string message) : base(message) { }
    }
}
