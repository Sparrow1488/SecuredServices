namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class RoleSessionManager : SessionManager
    {
        public RoleSessionManager(string id, string role) : base()
        {
            _id = id;
            _role = role;
        }

        private readonly string _id;
        private readonly string _role;

        public override void UpdateSession()
        {
            UserModel.Identificator = _id;
            UserModel.Policies = new string[] { _role };
            base.UpdateSession();
        }
    }
}
