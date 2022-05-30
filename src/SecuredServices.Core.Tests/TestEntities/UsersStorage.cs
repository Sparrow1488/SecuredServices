using SecuredServices.Core.Tests.TestPolicy.Roles;
using System.Collections.Generic;

namespace SecuredServices.Core.Tests.TestEntities
{
    public class UsersStorage
    {
        public IEnumerable<User> Users = new User[]
        {
            new User()
            {
                Id = 1,
                Role = TestSystemRole.User
            },
            new User()
            {
                Id = 2,
                Role = TestSystemRole.Moderator
            },
            new User()
            {
                Id = 3,
                Role = TestSystemRole.Administrator
            },
            new User()
            {
                Id = 4,
                Role = TestSystemRole.Creator
            },
            new User()
            {
                Id = 5,
                Role = TestSystemRole.User
            },
        };
    }
}
