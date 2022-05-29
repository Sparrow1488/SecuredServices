using System.Collections.Generic;

namespace SecuredServices.Core.Providers
{
    /// <summary>
    ///     2. Знает, какие права нужны, для внесения изменений
    /// </summary>
    public interface IPolicyProvider
    {
        IEnumerable<string> Policies { get; }
        int GetPolicyRank(string policy);
    }
}
