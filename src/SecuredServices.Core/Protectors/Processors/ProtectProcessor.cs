using System;

namespace SecuredServices.Core.Protectors.Processors
{
    /// <summary>
    ///     This class-protector secure your entity from changes
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ProtectProcessor<TEntity>
    {
        public ProtectProcessor(ISessionManager session) 
        {
            Session = session;
        }

        public abstract Type HandleAttributeType { get; }
        protected ISessionManager Session { get; }

        public abstract bool IsProtected(TEntity changed, TEntity initial);
    }
}
