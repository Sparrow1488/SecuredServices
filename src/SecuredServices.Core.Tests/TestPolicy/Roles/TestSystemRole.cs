using SecuredServices.Core.Attributes;

namespace SecuredServices.Core.Tests.TestPolicy.Roles
{
    public class TestSystemRole
    {
        [Policy(rank: 0)]
        public const string Anon = nameof(Anon);
        [Policy(rank: 1)]
        public const string User = nameof(User);
        [Policy(rank: 2)]
        public const string Moderator = nameof(Moderator);
        [Policy(rank: 3)]
        public const string Administrator = nameof(Administrator);
        [Policy(rank: 4)]
        public const string Creator = nameof(Creator);
    }
}
