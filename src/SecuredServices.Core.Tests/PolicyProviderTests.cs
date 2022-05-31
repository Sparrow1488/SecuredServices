using NUnit.Framework;
using SecuredServices.Core.Providers;
using SecuredServices.Core.Tests.TestPolicy.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecuredServices.Core.Tests
{
    internal class PolicyProviderTests
    {
        [Test]
        public void CtorWithOneType_TypeWithNoRoles_DoesNotThrow()
        {
            var typeOfRoles = typeof(TestEmptyRole);

            Assert.DoesNotThrow(() =>
            {
                new PolicyProvider(typeOfRoles);
            });
        }

        [Test]
        public void CtorWithOneType_TypeWithNoRoles_PoliciesEnumerableNotNull()
        {
            var typeOfRoles = typeof(TestEmptyRole);

            var policyProvider = new PolicyProvider(typeOfRoles);

            Assert.NotNull(policyProvider.Policies);
        }

        [Test]
        public void CtorWithSomeTypes_SomeRolesTypes_PoliciesEnumerableNotNull()
        {
            var typeOfRolesFirst = typeof(TestOneRole);
            var typeOfRolesSecond = typeof(TestTwoRole);
            var rolesTypes = new Type[] { typeOfRolesFirst, typeOfRolesSecond };

            var policyProvider = new PolicyProvider(rolesTypes);

            Assert.NotNull(policyProvider.Policies);
        }

        [Test]
        public void GetPolicyRank_NameAndTypeOfTestOneRole_CorrectPolicyRank()
        {
            const int expectedPolicyRank = 1;
            var rolesTypes = CreatePoliciesTypes(typeof(TestOneRole), typeof(TestTwoRole));
            var policyProvider = new PolicyProvider(rolesTypes);

            var policyRank = policyProvider.GetPolicyRank(TestOneRole.SomeRole, rolesTypes.First());

            Assert.AreEqual(expectedPolicyRank, policyRank);
        }

        [Test]
        public void GetPolicyRank_FullNameOfPolicy_CorrectPolicyRank()
        {
            const int expectedPolicyRank = 1;
            var rolesTypes = CreatePoliciesTypes(typeof(TestOneRole), typeof(TestTwoRole));
            var policyProvider = new PolicyProvider(rolesTypes);
            var findPolicyFullName = $"{rolesTypes.First().Name}.{TestOneRole.SomeRole}";

            var policyRank = policyProvider.GetPolicyRank(findPolicyFullName);

            Assert.AreEqual(expectedPolicyRank, policyRank);
        }

        [Test]
        public void GetPolicyRank_NameOfPolicy_DefaultValueBecauseMoreThanOnePolicyType()
        {
            const int expectedDefaultPolicyRank = 0;
            var rolesTypes = CreatePoliciesTypes(typeof(TestOneRole), typeof(TestTwoRole));
            var policyProvider = new PolicyProvider(rolesTypes);

            var policyRank = policyProvider.GetPolicyRank(TestOneRole.SomeRole);

            Assert.AreEqual(expectedDefaultPolicyRank, policyRank);
        }

        [Test]
        public void GetPolicyRank_NameOfPolicy_PolicyRankBecauseOnlyOnePolicyType()
        {
            const int expectedPolicyRank = 1;
            var rolesTypes = CreatePoliciesTypes(typeof(TestOneRole));
            var policyProvider = new PolicyProvider(rolesTypes);

            var policyRank = policyProvider.GetPolicyRank(TestOneRole.SomeRole);

            Assert.AreEqual(expectedPolicyRank, policyRank);
        }

        private IEnumerable<Type> CreatePoliciesTypes(params Type[] types)
        {
            return new List<Type>(types);
        }
    }
}
