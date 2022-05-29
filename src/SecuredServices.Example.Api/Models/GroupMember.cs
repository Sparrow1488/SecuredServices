using SecuredServices.Example.Api.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecuredServices.Example.Api.Models
{
    public class GroupMember
    {
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public string Role { get; set; } = GroupRole.Member;
        public int GroupId { get; set; }
    }
}
