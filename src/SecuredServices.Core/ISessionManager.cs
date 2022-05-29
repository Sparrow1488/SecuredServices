namespace SecuredServices.Core
{
    /// <summary>
    ///     1. Знает, кто вносит изменения
    /// </summary>
    public interface ISessionManager
    {
        public bool IsAuthorized { get; }
        public string Role { get; }
    }
}
