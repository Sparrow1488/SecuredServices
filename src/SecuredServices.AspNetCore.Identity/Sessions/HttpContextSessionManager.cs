using Microsoft.AspNet.Identity;
using SecuredServices.Core;

namespace SecuredServices.AspNetCore.Identity.Sessions
{
    internal class IdentitySessionManager<TIdentity> : ISessionManager
        where TIdentity : IUser
    {
        public IdentitySessionManager()
        {
        }

        public int ClientId => throw new NotImplementedException();
        public string Role => throw new NotImplementedException();
        public bool IsAuthorized => throw new NotImplementedException();
    }
}
