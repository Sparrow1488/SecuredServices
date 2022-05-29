using Microsoft.AspNetCore.Mvc;
using SecuredServices.Example.Api.Data;
using SecuredServices.Example.Api.Models;
using SecuredServices.Example.Api.Roles;

namespace SecuredServices.Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        [HttpGet]
        public IActionResult Index()
        {
            var user = new User() { Name = "User-name" };
            var group = new Group()
            {
                Title = "TestGroup",
                Description = "No desc",
                Members = new GroupMember[] {
                    new GroupMember() {
                        Id = user.Id,
                        Role = GroupRole.Editor
                    }
                }
            };
            _context.Users.Add(user);
            _context.Groups.Add(group);

            user.GroupMemberId = group.Members.Where(x => x.Id == user.Id).First().Id;
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(new
            {
                Ok = true
            });
        }
    }
}
