using SecuredServices.Core.Attributes;

namespace SecuredServices.Core.Tests.TestPolicy.Roles
{
    internal class TestDoubleRole
    {
        [Policy(rank: 1)]
        public const string SomeRole = nameof(SomeRole);
    }

    internal class TestDoubleTwoRole
    {
        [Policy(rank: 1)]
        public const string SomeRole = nameof(SomeRole);
    }
}
