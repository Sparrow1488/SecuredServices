using SecuredServices.Core.Attributes;
using SecuredServices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecuredServices.Core.Providers
{
    public class PolicyProviderFeature : IPolicyProvider
    {
        public PolicyProviderFeature(Type entityWithPolicies)
        {
            _policiesTypes = new Type[] { entityWithPolicies };
            _policiesInfos = GetPolicies();
        }

        public PolicyProviderFeature(IEnumerable<Type> entitiesWithPolicies)
        {
            _policiesTypes = entitiesWithPolicies;
            _policiesInfos = GetPolicies();
        }

        private readonly IList<PolicyInfo> _policiesInfos;
        private readonly IEnumerable<Type> _policiesTypes;

        public IEnumerable<string> Policies => _policiesInfos.Select(x => x.FullName) ?? Array.Empty<string>();

        public int GetPolicyRank(string policyFullName)
        {
            const int defaultPolicyRankValue = 0;
            var policyRank = defaultPolicyRankValue;
            var splitedPolicyFullName = policyFullName.Split(".");
            if (splitedPolicyFullName.Length == 2)
            {
                var policyTypeName = splitedPolicyFullName[0];
                var policyName = splitedPolicyFullName[1];
                var foundPolicyInfo = _policiesInfos.SingleOrDefault(
                                        x => x.Name == policyName && x.PolicyType.Name == policyTypeName);
                if (foundPolicyInfo is not null)
                {
                    policyRank = foundPolicyInfo.Rank;
                }
            }
            return policyRank;
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
