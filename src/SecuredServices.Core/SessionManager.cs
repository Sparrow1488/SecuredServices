using System.Threading.Tasks;

namespace SecuredServices.Core
{
    public class SessionManager : ISessionManager
    {
        /// <summary>
        ///     <see cref="UpdateSession"/> on create instance of this object
        /// </summary>
        public SessionManager()
        {
            UpdateSession();
        }

        public bool IsAuthorized { get; protected set; }
        public string Role { get; protected set; }
        public int ClientId { get; protected set; }

        public virtual void UpdateSession() { }
        public virtual Task UpdateSessionAsync() =>
            Task.CompletedTask;
    }
}
