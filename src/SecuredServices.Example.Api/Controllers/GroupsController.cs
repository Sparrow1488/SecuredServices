using Microsoft.AspNetCore.Mvc;
using SecuredServices.Core.Exceptions;
using SecuredServices.Example.Api.Data;
using SecuredServices.Example.Api.Services;

namespace SecuredServices.Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        public GroupsController(
            ApplicationDbContext context,
            GroupsService service,
            IHttpContextAccessor accessor)
        {
            _context = context;
            _service = service;
        }

        private readonly ApplicationDbContext _context;
        private readonly GroupsService _service;

        [HttpGet]
        public IActionResult Index([FromQuery]int userId, [FromQuery]int groupId)
        {
            var editedGroup = _context.Groups.First(x => x.Id == groupId).Clone();
            editedGroup.Title = "Changed title " + Random.Shared.Next();
            try
            {
                _service.Edit(editedGroup);
            }
            catch (AccessDeniedException ex)
            {
                return BadRequest(new
                {
                    Ok = false,
                    Errors = new string[] { ex.Message }
                });
            }

            return Ok(new { Ok = true });
        }
    }
}
