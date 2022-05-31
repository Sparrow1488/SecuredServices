using System;
using System.Collections.Generic;

namespace SecuredServices.Core.Models
{
    public class DefaultUserModel : UserModel
    {
        public DefaultUserModel() { }

        public override string Identificator
        {
            get => base.Identificator ?? string.Empty;
            set => base.Identificator = value;
        }
        public override IEnumerable<string> Policies 
        {
            get => base.Policies ?? Array.Empty<string>(); 
            set => base.Policies = value; 
        }
    }
}
