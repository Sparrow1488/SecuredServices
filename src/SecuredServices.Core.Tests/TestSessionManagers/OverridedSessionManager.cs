using SecuredServices.Core.Models;
using SecuredServices.Core.Tests.TestPolicy.Roles;
using System;

namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class OverridedSessionManager : SessionManager
    {
        public OverridedSessionManager() : base() { }

        public override void UpdateSession()
        {
            UserModel.Policies = new string[] { TestSystemRole.User };
            UserModel.Identificator = Guid.NewGuid().ToString();
            IsAuthorized = true;
            base.UpdateSession();
        }
    }
}
