namespace SecuredServices.Core
{
    /// <summary>
    ///     1. Знает, кто вносит изменения
    /// </summary>
    public interface ISessionManager
    {
        public int ClientId { get; }
        public string Role { get; }
        public bool IsAuthorized { get; }
    }
}
