using Microsoft.AspNetCore.Http;

namespace SecuredServices.Core
{
    public class AspNetSessionManager
    {
        public AspNetSessionManager(
            IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private readonly IHttpContextAccessor _contextAccessor;
    }
}
