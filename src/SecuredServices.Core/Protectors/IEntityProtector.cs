using SecuredServices.Core.Messages;
using System.Collections.Generic;

namespace SecuredServices.Core.Protectors
{
    public interface IEntityProtector<TEntity>
    {
        public IEnumerable<IProtectorMessage> Messages { get; }
        bool IsProtected(TEntity toCheck, TEntity initial);
        bool IsProtected(TEntity toCheck);
    }
}
