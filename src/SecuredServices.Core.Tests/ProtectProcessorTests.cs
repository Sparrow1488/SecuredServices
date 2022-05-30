using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SecuredServices.Core.Protectors;
using SecuredServices.Core.Providers;
using SecuredServices.Core.Tests.TestEntities;
using SecuredServices.Core.Tests.TestPolicy.Roles;
using SecuredServices.Core.Tests.TestProcessors;
using SecuredServices.Core.Tests.TestSessionManagers;
using System.Linq;

namespace SecuredServices.Core.Tests
{
    internal class ProtectProcessorTests
    {
        private IServiceCollection _services;
        private IServiceScope _scope => _services.BuildServiceProvider().CreateScope();

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<GroupsStorage>();
            _services.AddSingleton<UsersStorage>();
            _services.AddTransient<IEntityProtector<Group>, EntityProtector<Group>>();
            _services.AddTransient<IPolicyProvider, PolicyProvider>(x => new PolicyProvider(typeof(TestGroupRole)));
        }

        [Test]
        public void IsProtected_EditedGroupByAnotherGroupMember_False()
        {
            var initGroup = GetGroupById(1);
            RegisterGroupSessionManager(4);
            _services.AddTransient<GroupChangeProtectProcessor>();
            var editedGroup = initGroup.Clone();
            var groupChangeProcessor = ActivatorUtilities.CreateInstance<GroupChangeProtectProcessor>(_scope.ServiceProvider);

            var isProtected = groupChangeProcessor.IsProtected(editedGroup, initGroup);

            Assert.False(isProtected);
        }

        [Test]
        public void IsProtected_EditedGroupByGroupMember_True()
        {
            var initGroup = GetGroupById(1);
            RegisterGroupSessionManager(5);
            _services.AddTransient<GroupChangeProtectProcessor>();
            var editedGroup = initGroup.Clone();
            var groupChangeProcessor = ActivatorUtilities.CreateInstance<GroupChangeProtectProcessor>(_scope.ServiceProvider);

            var isProtected = groupChangeProcessor.IsProtected(editedGroup, initGroup);

            Assert.True(isProtected);
        }

        private Group GetGroupById(int id)
        {
            var groupsStorage = _scope.ServiceProvider.GetService<GroupsStorage>();
            return groupsStorage.Groups.First(x => x.Id == id);
        }

        private User GetUserById(int id)
        {
            var usersStorage = _scope.ServiceProvider.GetService<UsersStorage>();
            return usersStorage.Users.First(x => x.Id == id);
        }
        private void RegisterGroupSessionManager(int currentUser)
        {
            _services.AddTransient<ISessionManager, AlternativeGroupSessionManager>(
                    x => new AlternativeGroupSessionManager(currentUser));
        }
    }
}
