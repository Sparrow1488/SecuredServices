using System.Collections.Generic;

namespace SecuredServices.Core.Models
{
    public abstract partial class UserModel
    {
        public virtual string Identificator { get; set; }
        public virtual IEnumerable<string> Policies { get; set; }
    }
}
