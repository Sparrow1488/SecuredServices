using System.Collections.Generic;

namespace SecuredServices.Core.Models
{
    public abstract partial class UserModel<TId> : UserModel
    {
        public TId Id { get; protected set; }
        public override object Identificator => Id;
    }

    public abstract partial class UserModel
    {
        public virtual object Identificator { get; protected set; }
        public IEnumerable<string> Policies { get; protected set; }
    }
}
