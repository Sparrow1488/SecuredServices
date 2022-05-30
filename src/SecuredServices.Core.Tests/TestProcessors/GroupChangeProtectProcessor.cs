using SecuredServices.Core.Attributes;
using SecuredServices.Core.Protectors.Processors;
using SecuredServices.Core.Tests.TestEntities;
using System;
using System.Linq;

namespace SecuredServices.Core.Tests.TestProcessors
{
    internal class GroupChangeProtectProcessor : ProtectProcessor<Group>
    {
        public GroupChangeProtectProcessor(
            ISessionManager session, 
            GroupsStorage groupsStorage) : base(session)
        {
            _groupsStorage = groupsStorage;
        }

        private readonly GroupsStorage _groupsStorage;

        public override Type HandleAttributeType => typeof(ChangeProtectionAttribute);

        public override bool IsProtected(Group changed, Group initial)
        {
            var groupFromDb = _groupsStorage.Groups.First(x => x.Id == changed.Id);
            if (groupFromDb.Users.Any(x => x.Id == Session.ClientId))
            {
                return true; // состоит в группе, поэтому разрешаем ему вносить изменения
            }
            return false; // не состоит в данной группе
        }
    }
}
