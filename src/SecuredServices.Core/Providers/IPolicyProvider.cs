using SecuredServices.Core.Attributes;
using SecuredServices.Core.Protectors;
using System;
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
        ///     <para>
        ///         Get your policy rank from <see cref="PolicyAttribute"/>(rank)
        ///     </para>
        ///     <para>
        ///         But recommended to use is <seealso cref="GetPolicyRank(string, Type)"/>
        ///     </para>
        /// </summary>
        /// <param name="policy">Policy that contains in <see cref="Policies"/></param>
        /// <returns>Policy rank number</returns>
        int GetPolicyRank(string policy);
        /// <summary>
        ///     Get your policy rank from <see cref="PolicyAttribute"/>(rank) in your <paramref name="policyType"/>
        /// </summary>
        /// <param name="policy">Policy that contains in <see cref="Policies"/></param>
        /// <returns>Policy rank number</returns>
        int GetPolicyRank(string policy, Type policyType);
    }
}
