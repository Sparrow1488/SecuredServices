using SecuredServices.Core.Models;
using System.Threading.Tasks;

namespace SecuredServices.Core
{
    public class SessionManager : ISessionManager
    {
        public SessionManager() { }

        private UserModel _userModel;
        private bool _isAuthorized;

        public virtual bool IsAuthorized
        {
            get => _isAuthorized;
            set => _isAuthorized = value;
        }
        public virtual UserModel UserModel
        {
            get => _userModel ?? (_userModel = new DefaultUserModel()); 
            protected set => _userModel = value;
        }

        public virtual void UpdateSession() { }
        public virtual async Task UpdateSessionAsync() =>
            await Task.Run(() => UpdateSession());
    }
}
