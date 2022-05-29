using SecuredServices.Core.Tests.TestPolicy.Roles;
using System.Collections.Generic;

namespace SecuredServices.Core.Tests.TestEntities
{
    internal class GroupsStorage
    {
        public IEnumerable<Group> Groups = new Group[]
        {
            new Group() 
            { 
                Id = 1, 
                Members = new GroupMember[]
                { 
                    new GroupMember() { Id = 1, GroupRole = TestGroupRole.User },
                    new GroupMember() { Id = 2, GroupRole = TestGroupRole.Editor },
                }
            }
        };
    }
}
