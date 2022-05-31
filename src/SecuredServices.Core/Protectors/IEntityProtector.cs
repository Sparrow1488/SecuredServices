using SecuredServices.Core.Messages;
using SecuredServices.Core.Protectors.Processors;
using SecuredServices.Core.Providers;
using System.Collections.Generic;

namespace SecuredServices.Core.Protectors
{
    /// <summary>
    ///     This interface can be used as shell for <see cref="IPolicyProvider"/>, 
    ///     <see cref="ProtectProcessor{TEntity}"/> and <see cref="ISessionManager"/>.
    ///     it's meant to protect system entities from changes by different users with using policies.
    /// </summary>
    /// <typeparam name="TEntity">Type of system entity to protect.</typeparam>
    public interface IEntityProtector<TEntity>
    {
        /// <summary>
        ///     This messages give extended information about entity protection 
        ///     (call <see cref="IsProtected(TEntity)"/>) or <see cref="IsProtected(TEntity, TEntity)"/> to update messages.
        /// </summary>
        public IEnumerable<IProtectorMessage> Messages { get; }
        /// <summary>
        ///     Check your entity instance protection. It can be compare with initial state of entity instance.
        /// </summary>
        /// <param name="toCheck">Changed object that can be compared to an <paramref name="initial"/>.</param>
        /// <param name="initial">Can be compared with <paramref name="toCheck"/>.</param>
        /// <returns>Is <paramref name="toCheck"/> protected.</returns>
        bool IsProtected(TEntity toCheck, TEntity initial);
        /// <summary>
        ///     Check your entity instance protection.
        /// </summary>
        /// <param name="toCheck">Entity to check protection.</param>
        /// <returns>Is <paramref name="toCheck"/> protected.</returns>
        bool IsProtected(TEntity toCheck);
    }
}
