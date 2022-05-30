using SecuredServices.Core.Tests.TestEntities;
using System.Linq;
using System.Threading.Tasks;

namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class GroupSessionManager : ISessionManager
    {
        public GroupSessionManager(
            int currentUserId,
            int currentGroupId,
            GroupsStorage storage) // можно также получить из HttpContext, в после из базы данных (если работаем через AspNetCore)
        {
            var group = storage.Groups.First(x => x.Id == currentGroupId);
            _member = group.Members.First(x => x.Id == currentUserId);
        }

        private readonly GroupMember _member;

        public bool IsAuthorized => _member == null;
        public string Role => _member.GroupRole;
        public int ClientId => _member.Id;

        public void UpdateSession()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSessionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
