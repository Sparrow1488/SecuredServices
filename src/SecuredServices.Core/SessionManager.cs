using SecuredServices.Core.Models;
using System.Threading.Tasks;

namespace SecuredServices.Core
{
    public class SessionManager : ISessionManager
    {
        /// <summary>
        ///     <see cref="UpdateSession"/> on create instance of this object
        /// </summary>
        public SessionManager()
        {
            UpdateSession();
        }

        private UserModel _userModel;
        private bool _isAuthorized;

        public bool IsAuthorized
        {
            get => _isAuthorized;
            set => _isAuthorized = value;
        }
        public UserModel UserModel
        {
            get => _userModel ?? (_userModel = new DefaultUserModel()); 
            set => _userModel = value;
        }

        public virtual void UpdateSession() { }
        public virtual Task UpdateSessionAsync() =>
            Task.CompletedTask;
    }
}
