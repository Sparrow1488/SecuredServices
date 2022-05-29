using System;

namespace SecuredServices.Core.Exceptions
{
    public class SecuredServiceException : Exception
    {
        public SecuredServiceException() { }
        public SecuredServiceException(string message) : base(message) { }
    }
}
