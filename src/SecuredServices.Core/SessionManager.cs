namespace SecuredServices.Core
{
    /// <summary>
    ///     Контекст с клиентом, который использует SecuredServices
    /// </summary>
    public class SessionManager : ISessionManager
    {
        public bool IsAuthorized { get; set; }
        public string Role { get; set; }
        public int ClientId { get; set; }
    }
}
