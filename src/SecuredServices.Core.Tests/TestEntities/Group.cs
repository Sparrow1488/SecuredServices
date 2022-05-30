using SecuredServices.Core.Attributes;
using SecuredServices.Core.Tests.TestPolicy.Roles;
using System.Collections.Generic;

namespace SecuredServices.Core.Tests.TestEntities
{
    [ChangeProtection(TestSystemRole.User)]
    public class Group : Identity
    {
        [ChangeProtection(TestGroupRole.Editor)]
        public string Title { get; set; }

        [ChangeProtection(TestGroupRole.Editor)]
        public string Description { get; set; }

        [ChangeProtection(TestGroupRole.Editor)]
        public bool IsPrivate { get; set; } = false;

        public IEnumerable<GroupMember> Members { get; set; }
        public IEnumerable<User> Users { get; set; }

        public Group Clone()
        {
            return new Group()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                IsPrivate = IsPrivate,
                Members = Members
            };
        }
    }
}
