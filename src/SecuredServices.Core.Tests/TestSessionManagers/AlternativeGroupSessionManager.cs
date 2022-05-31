namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class AlternativeGroupSessionManager : SessionManager
    {
        public AlternativeGroupSessionManager(
            int currentUserId)
        {
            _currentUserId = currentUserId;
            UpdateSession();
        }

        private readonly int _currentUserId;
        public override bool IsAuthorized => _currentUserId > 0;

        public override void UpdateSession()
        {
            UserModel.Identificator = _currentUserId.ToString();
            base.UpdateSession();
        }
    }
}
