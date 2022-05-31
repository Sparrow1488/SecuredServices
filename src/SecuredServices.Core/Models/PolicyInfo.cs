using SecuredServices.Core.Exceptions;
using System;

namespace SecuredServices.Core.Models
{
    internal class PolicyInfo
    {
        private PolicyInfo(string name, int rank, Type policyType)
        {
            Name = name;
            Rank = rank;
            PolicyType = policyType;
        }

        public string Name { get; }
        public int Rank { get; }
        public Type PolicyType { get; }
        public string FullName => $"{PolicyType.Name.ToString()}.{Name}";

        public static PolicyInfo CreateInfo(string name, int rank, Type policyType)
        {
            ThrowIfNameEmptyOrNull(name);
            ThrowIfPolicyTypeNull(policyType);
            return new PolicyInfo(name, rank, policyType);
        }

        private static void ThrowIfNameEmptyOrNull(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new CreatePolicyInfoException();
        }

        private static void ThrowIfPolicyTypeNull(Type policyType)
        {
            if (policyType is null)
                throw new CreatePolicyInfoException();
        }

        public override string ToString() => FullName;
    }
}
