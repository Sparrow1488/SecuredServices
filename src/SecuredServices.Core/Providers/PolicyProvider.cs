using SecuredServices.Core.Attributes;
using SecuredServices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecuredServices.Core.Providers
{
    public class PolicyProvider : IPolicyProvider
    {
        public PolicyProvider(Type entityWithPolicies)
        {
            _policiesTypes = new Type[] { entityWithPolicies };
            _policiesInfos = GetPolicies();
        }

        public PolicyProvider(IEnumerable<Type> entitiesWithPolicies)
        {
            _policiesTypes = entitiesWithPolicies;
            _policiesInfos = GetPolicies();
        }

        private readonly IList<PolicyInfo> _policiesInfos;
        private readonly IEnumerable<Type> _policiesTypes;

        public IEnumerable<string> Policies => _policiesInfos.Select(x => x.FullName) ?? Array.Empty<string>();

        /// <summary>
        ///     <para>
        ///         If in provider initialized only one type of policy then tou can get rank by <see cref="PolicyInfo.Name"/> or <see cref="PolicyInfo.FullName"/>
        ///     </para>
        ///     <para>
        ///         If in provider initialized more one policies types then tou can get rank only by <see cref="PolicyInfo.FullName"/>
        ///     </para>
        /// </summary>
        /// <param name="policyFullName">Full name of policy or just name</param>
        /// <returns></returns>
        public int GetPolicyRank(string policyFullName)
        {
            int policyRank = 0;
            var splitedPolicyFullName = policyFullName.Split(".");
            if (_policiesTypes.Count() > 1 || splitedPolicyFullName.Length == 2)
            {
                if (splitedPolicyFullName.Length == 2)
                {
                    var policyTypeName = splitedPolicyFullName[0];
                    var policyName = splitedPolicyFullName[1];
                    policyRank = GetPolicyRankByNameAndType(policyName, policyTypeName);
                }
            }
            else
            {
                policyRank = GetPolicyRankByName(policyFullName);
            }
            return policyRank;
        }

        private int GetPolicyRankByName(string policyName)
        {
            var foundPolicyInfo = _policiesInfos.SingleOrDefault(x => x.Name == policyName);
            return foundPolicyInfo?.Rank ?? 0;
        }

        private int GetPolicyRankByNameAndType(string policyName, string policyTypeName)
        {
            var foundPolicyInfo = _policiesInfos.SingleOrDefault(
                                        x => x.Name == policyName && x.PolicyType.Name == policyTypeName);
            return foundPolicyInfo?.Rank ?? 0;
        }

        public virtual int GetPolicyRank(string policy, Type policyType)
        {
            const int defaultPolicyRankValue = 0;
            var policyRank = defaultPolicyRankValue;
            var foundPolicyInfo = _policiesInfos.SingleOrDefault(x => x.Name == policy && x.PolicyType == policyType);
            if (foundPolicyInfo is not null)
            {
                policyRank = foundPolicyInfo.Rank;
            }
            return policyRank;
        }

        private IList<PolicyInfo> GetPolicies()
        {
            var policies = new List<PolicyInfo>();
            foreach (var typeWithPolicies in _policiesTypes)
            {
                var policiesProps = GetPolicyProperties(typeWithPolicies);
                foreach (var prop in policiesProps)
                {
                    var attribute = prop.GetCustomAttribute<PolicyAttribute>();
                    policies.Add(PolicyInfo.CreateInfo(prop.Name, attribute.Rank, typeWithPolicies));
                }
            }
            return policies;
        }

        private IEnumerable<FieldInfo> GetPolicyProperties(Type policyType)
        {
            return policyType.GetFields()
                .Where(x => x.GetCustomAttribute<PolicyAttribute>() is not null);
        }
    }
}
