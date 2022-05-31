using SecuredServices.Core.Attributes;
using SecuredServices.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecuredServices.Core.Providers
{
    public class PolicyProvider : IPolicyProvider
    {
        public PolicyProvider(Type entityWithPolicies) // TODO: добавить поддержку нескольких типов, поддерживающих PolicyAttribute (ex. GroupRole, SystemRole)
        {
            _entityWithPolicies = entityWithPolicies;
            _policiesWithRanks = GetPolicies();
        }

        private readonly IDictionary<string, int> _policiesWithRanks;
        private readonly Type _entityWithPolicies;

        public IEnumerable<string> Policies => _policiesWithRanks.Select(x => x.Key);

        public virtual int GetPolicyRank(string policy)
        {
            policy = policy ?? string.Empty;
            _policiesWithRanks.TryGetValue(policy, out int policyRank);
            return policyRank;
        }

        private IDictionary<string, int> GetPolicies()
        {
            var dictionary = new Dictionary<string, int>();
            var policiesProps = GetPolicyProperties();
            foreach (var prop in policiesProps)
            {
                var attribute = prop.GetCustomAttribute<PolicyAttribute>();
                var canAddPolicy = dictionary.TryAdd(prop.Name, attribute.Rank);
                ThrowIfCantAddNewPolicy(canAddPolicy);
            }
            return dictionary;
        }

        private IEnumerable<FieldInfo> GetPolicyProperties()
        {
            return _entityWithPolicies.GetFields()
                .Where(x => x.GetCustomAttribute<PolicyAttribute>() is not null);
        }

        /// <exception cref="FailedUsePolicyException"></exception>
        private void ThrowIfCantAddNewPolicy(bool canAddPolicy, string errorMessage = null)
        {
            if (!canAddPolicy)
            {
                errorMessage = errorMessage ?? $"Can't be use one or more policies from current type {_entityWithPolicies.ToString()}";
                throw new FailedUsePolicyException(errorMessage);
            }
        }
    }
}
