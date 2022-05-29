using SecuredServices.Core.Attributes;

namespace SecuredServices.Example.Api.Roles
{
    public class GroupRole
    {
        [Policy(rank: 1)]
        public const string Member = nameof(Member);
        [Policy(rank: 2)]
        public const string Editor = nameof(Editor);
        [Policy(rank: 3)]
        public const string Creator = nameof(Creator);
    }
}
