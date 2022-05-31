using SecuredServices.Core.Attributes;

namespace SecuredServices.Core.Tests.TestPolicy.Roles
{
    internal class TestOneRole
    {
        [Policy(rank: 1)]
        public const string SomeRole = nameof(SomeRole);
    }

    internal class TestTwoRole
    {
        [Policy(rank: 1)]
        public const string SomeRole = nameof(SomeRole);
    }
}
