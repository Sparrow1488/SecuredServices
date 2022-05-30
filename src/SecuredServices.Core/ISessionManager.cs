using SecuredServices.Core.Models;
using SecuredServices.Core.Protectors;
using System.Threading.Tasks;

namespace SecuredServices.Core
{
    /// <summary>
    ///     This interface provides information about a client that wants to modify a protected object. 
    ///     Use it for provide client information in the <see cref="IEntityProtector{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TUser">Type of <see cref="UserModel{TId}"/> that can store user id, policies and other custom properties</typeparam>
    public interface ISessionManager<TUser> : ISessionManager
        where TUser : UserModel
    {
        public TUser UserModel { get; }
    }

    /// <summary>
    ///     This interface provides information about a client that wants to modify a protected object. 
    ///     Use it for provide client information in the <see cref="IEntityProtector{TEntity}"/>.
    /// </summary>
    public interface ISessionManager
    {
        public int ClientId { get; } // TODO: по хорошему этого быть не должно, тк id могут быть разными на проектах
        public string Role { get; } // TODO: да и этого тоже. Все спихнуть в UserModel
        public bool IsAuthorized { get; }

        void UpdateSession();
        Task UpdateSessionAsync();
    }
}
