using SecuredServices.Core.Tests.TestEntities;
using System.Linq;

namespace SecuredServices.Core.Tests.TestSessionManagers
{
    internal class GroupSessionManager : SessionManager
    {
        public GroupSessionManager(
            int currentUserId,
            int currentGroupId,
            GroupsStorage storage)
        {
            var group = storage.Groups.First(x => x.Id == currentGroupId);
            _member = group.Members.First(x => x.Id == currentUserId);
            UpdateSession();
        }

        private readonly GroupMember _member;

        public override bool IsAuthorized => _member.Id > 0;

        public override void UpdateSession()
        {
            UserModel.Identificator = _member.Id.ToString();
            UserModel.Policies = new string[]
            {
                _member.GroupRole
            };
        }
    }
}
