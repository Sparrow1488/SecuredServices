using NUnit.Framework;
using SecuredServices.Core.Providers;
using SecuredServices.Core.Tests.TestPolicy.Roles;

namespace SecuredServices.Core.Tests
{
    internal class PolicyProviderTests
    {
        [Test]
        public void Ctor_TypeWithNoRoles_DoesNotThrow()
        {
            var typeOfRoles = typeof(TestEmptyRole);

            Assert.DoesNotThrow(() =>
            {
                new PolicyProvider(typeOfRoles);
            });
        }

        [Test]
        public void Ctor_TypeWithNoRoles_PoliciesEnumerableNotNull()
        {
            var typeOfRoles = typeof(TestEmptyRole);

            var policyProvider = new PolicyProvider(typeOfRoles);

            Assert.NotNull(policyProvider.Policies);
        }
    }
}
