using System.ComponentModel.DataAnnotations.Schema;

namespace SecuredServices.Example.Api.Models
{
    public class User : Identity
    {
        public string Name { get; set; }
        [ForeignKey(nameof(GroupMember))]
        public int? GroupMemberId { get; set; }
        // данное свойство приведено в качестве примера, чтобы показать альтернативную возможность использования SecuredServices
        public int? GroupId { get; set; }
    }
}
