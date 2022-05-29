using SecuredServices.Core.Exceptions;
using SecuredServices.Core.Protectors;
using SecuredServices.Example.Api.Data;
using SecuredServices.Example.Api.Models;

namespace SecuredServices.Example.Api.Services
{
    public class GroupsService
    {
        public GroupsService(
            ApplicationDbContext context,
            IEntityProtector<Group> protector)
        {
            _context = context;
            _protector = protector;
        }

        private readonly ApplicationDbContext _context;
        private readonly IEntityProtector<Group> _protector;

        /// <summary> Change group properties </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AccessDeniedException"></exception>
        public Group Edit(Group changed)
        {
            var dbGroup = _context.Groups.FirstOrDefault(g => g.Id == changed.Id) 
                            ?? throw new ArgumentException($"{nameof(changed)}.Id not exists");

            ThrowIfGroupNotProtected(changed, dbGroup);

            dbGroup.Title = changed.Title;
            dbGroup.Description = changed.Description;
            _context.Update(dbGroup);
            _context.SaveChanges();

            return dbGroup;
        }

        private void ThrowIfGroupNotProtected(Group changed, Group initial)
        {
            if (!_protector.IsProtected(changed, initial))
                throw new AccessDeniedException("User role less to change information this group!");
        }
    }
}
