using SecuredServices.Core.Models;
using SecuredServices.Core.Protectors;
using System.Threading.Tasks;

namespace SecuredServices.Core
{
    /// <summary>
    ///     This interface provides information about a client that wants to modify a protected object. 
    ///     Use it for provide client information in the <see cref="IEntityProtector{TEntity}"/>.
    /// </summary>
    public interface ISessionManager
    {
        public bool IsAuthorized { get; }
        public UserModel UserModel { get; }

        void UpdateSession();
        Task UpdateSessionAsync();
    }
}
