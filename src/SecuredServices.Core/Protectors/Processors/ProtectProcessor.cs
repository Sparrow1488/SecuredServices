using System;

namespace SecuredServices.Core.Protectors.Processors
{
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
