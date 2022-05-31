using NUnit.Framework;
using SecuredServices.Core.Tests.TestSessionManagers;
using System;
using System.Threading.Tasks;

namespace SecuredServices.Core.Tests
{
    internal class SessionManagerTests
    {
        [Test]
        public void UpdateSession_UpdatedUserModel()
        {
            var session = new OverridedSessionManager();

            session.UpdateSession();

            Assert.True(session.IsAuthorized);
            CollectionAssert.IsNotEmpty(session.UserModel.Policies);
            Assert.DoesNotThrow(() => Guid.Parse(session.UserModel.Identificator));
        }

        [Test]
        public async Task UpdateSessionAsync_UpdatedUserModel()
        {
            var session = new OverridedSessionManager();

            await session.UpdateSessionAsync();

            Assert.True(session.IsAuthorized);
            CollectionAssert.IsNotEmpty(session.UserModel.Policies);
            Assert.DoesNotThrow(() => Guid.Parse(session.UserModel.Identificator));
        }
    }
}
