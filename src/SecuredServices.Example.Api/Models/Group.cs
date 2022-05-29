using SecuredServices.Core.Attributes;
using SecuredServices.Example.Api.Roles;

namespace SecuredServices.Example.Api.Models
{
    public class Group : Identity
    {
        [ChangeProtection(GroupRole.Editor)]
        public string Title { get; set; }
        [ChangeProtection(GroupRole.Editor)]
        public string Description { get; set; }

        public IEnumerable<GroupMember> Members { get; set; }

        public Group Clone()
        {
            return new Group()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Members = Members
            };
        }
    }
}
