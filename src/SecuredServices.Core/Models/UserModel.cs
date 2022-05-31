using System.Collections.Generic;

namespace SecuredServices.Core.Models
{
    public abstract partial class UserModel
    {
        public virtual string Identificator { get; protected set; }
        public IEnumerable<string> Policies { get; protected set; }
    }
}
