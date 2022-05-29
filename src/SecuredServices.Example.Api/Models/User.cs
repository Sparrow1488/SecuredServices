using System.ComponentModel.DataAnnotations.Schema;

namespace SecuredServices.Example.Api.Models
{
    public class User : Identity
    {
        public string Name { get; set; }
        [ForeignKey(nameof(GroupMember))]
        public int? GroupMemberId { get; set; }
    }
}
