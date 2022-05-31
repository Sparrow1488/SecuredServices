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
        /// <summary>
        ///     Flag indicates whether the given user session information is up to date
        /// </summary>
        public bool IsAuthorized { get; }
        /// <summary>
        ///     Model of client info. Can provide client specifics.
        /// </summary>
        public UserModel UserModel { get; }

        /// <summary>
        ///     Update client session if needed.
        /// </summary>
        void UpdateSession();
        /// <summary>
        ///     Update client session async if needed.
        /// </summary>
        Task UpdateSessionAsync();
    }
}
