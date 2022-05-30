using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SecuredServices.Core.Protectors;
using SecuredServices.Core.Providers;
using SecuredServices.Core.Tests.TestEntities;
using SecuredServices.Core.Tests.TestPolicy.Roles;
using SecuredServices.Core.Tests.TestSessionManagers;
using System.Linq;

namespace SecuredServices.Core.Tests
{
    public class EntityProtectorTests
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
        public void IsProtected_EditedGroupByUser_False()
        {
            var initial = new Group() { Title = "Hello group!" };
            var edited = new Group() { Title = "Edited group title" };
            RegisterSessionManager(role: TestGroupRole.User);
            var protector = _scope.ServiceProvider.GetService<IEntityProtector<Group>>();

            var isProtected = protector.IsProtected(edited, initial);

            Assert.False(isProtected);
        }

        [Test]
        public void IsProtected_EditedGroupByEditor_True()
        {
            var initial = new Group() { Title = "Hello group!" };
            var edited = new Group() { Title = "Edited group title" };
            RegisterSessionManager(role: TestGroupRole.Editor);
            var protector = _scope.ServiceProvider.GetService<IEntityProtector<Group>>();

            var isProtected = protector.IsProtected(edited, initial);

            Assert.True(isProtected);
        }

        [Test]
        public void IsProtected_EditedGroupByGroupUser_False()
        {
            var toChangeGroupInitial = GetGroupById(1);
            var applyerChanges = GetUserById(1);         // можно получать из куки аутентификации автоматически
            RegisterGroupSessionManager(applyerChanges.Id, toChangeGroupInitial.Id);
            var protector = _scope.ServiceProvider.GetService<IEntityProtector<Group>>();

            var changedGroupClone = toChangeGroupInitial.Clone();
            changedGroupClone.Title = "Edited";
            var isProtected = protector.IsProtected(changedGroupClone, toChangeGroupInitial);

            Assert.False(isProtected);
        }

        [Test]
        public void IsProtected_EditedGroupByGroupEditor_True()
        {
            var toChangeGroupInitial = GetGroupById(1);
            var applyerChanges = GetUserById(2);
            RegisterGroupSessionManager(applyerChanges.Id, toChangeGroupInitial.Id);
            var protector = _scope.ServiceProvider.GetService<IEntityProtector<Group>>();

            var changedGroupClone = toChangeGroupInitial.Clone();
            changedGroupClone.Title = "Edited";
            var isProtected = protector.IsProtected(changedGroupClone, toChangeGroupInitial);

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

        private void RegisterSessionManager(string role = TestGroupRole.User)
        {
            _services.AddTransient<ISessionManager, SessionManager>(x => new SessionManager()
            {
                IsAuthorized = true,
                Role = role
            });
        }

        private void RegisterGroupSessionManager(int currentUserId, int groupId)
        {
            var storage = _scope.ServiceProvider.GetService<GroupsStorage>();
            _services.AddTransient<ISessionManager, GroupSessionManager>(
                x => new GroupSessionManager(currentUserId, groupId, storage));
        }
    }
}