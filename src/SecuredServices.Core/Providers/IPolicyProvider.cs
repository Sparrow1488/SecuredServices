using SecuredServices.Core.Attributes;
using SecuredServices.Core.Protectors;
using System.Collections.Generic;

namespace SecuredServices.Core.Providers
{
    /// <summary>
    ///     This interface provides your application policies to use it in different <see cref="IEntityProtector{TEntity}"/>
    /// </summary>
    public interface IPolicyProvider
    {
        /// <summary>
        ///     Your application policies
        /// </summary>
        IEnumerable<string> Policies { get; }
        /// <summary>
        ///     Set your policy rank using <see cref="PolicyAttribute"/>(rank)
        /// </summary>
        /// <param name="policy">Policy that contains in <see cref="Policies"/></param>
        /// <returns>Policy rank number</returns>
        int GetPolicyRank(string policy);
    }
}
