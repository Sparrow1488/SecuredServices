namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class AlternativeGroupSessionManager : ISessionManager
    {
        public AlternativeGroupSessionManager(
            int currentUserId)
        {
            _currentUserId = currentUserId;
        }

        private readonly int _currentUserId;

        public int ClientId => _currentUserId;
        public string Role => string.Empty;
        public bool IsAuthorized => _currentUserId > 0;
    }
}
