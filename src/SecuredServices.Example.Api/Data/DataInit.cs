using SecuredServices.Example.Api.Models;
using SecuredServices.Example.Api.Roles;

namespace SecuredServices.Example.Api.Data
{
    public class DataInit
    {
        private readonly ApplicationDbContext _context;

        public DataInit(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Init()
        {
            var user = new User() { Name = "EditorUser" };
            var user2 = new User() { Name = "MemberUser" };
            var group = new Group()
            {
                Title = "TestGroup",
                Description = "No desc",
                Members = new GroupMember[] {
                    new GroupMember() {
                        Id = user.Id,
                        Role = GroupRole.Editor
                    },
                    new GroupMember(){
                       Id = user2.Id,
                       Role = GroupRole.Member
                    }
                }
            };
            _context.Users.Add(user);
            _context.Users.Add(user2);
            _context.Groups.Add(group);
            _context.SaveChanges();
        }
    }
}
