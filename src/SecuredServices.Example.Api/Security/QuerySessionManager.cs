using SecuredServices.Core;
using SecuredServices.Example.Api.Data;

namespace SecuredServices.Example.Api.Security
{
    public class QuerySessionManager : SessionManager
    {
        public QuerySessionManager(
            IServiceProvider services,
            ApplicationDbContext context)
        {
            var accessor = services.CreateScope().ServiceProvider.GetService<IHttpContextAccessor>();
            if (accessor is not null)
            {
                var userIdValues = accessor?.HttpContext?.Request?.Query["userId"] ?? string.Empty;
                var groupIdValues = accessor?.HttpContext?.Request?.Query["groupId"] ?? string.Empty;
                if (userIdValues.Count > 0)
                {
                    var first = userIdValues.FirstOrDefault();
                    int.TryParse(first, out _userId);
                    if (groupIdValues.Count > 0)
                    {
                        first = userIdValues.FirstOrDefault();
                        if (first is not null)
                        {
                            var groupMember = context.GroupMembers.Where(x => x.Id == _userId).FirstOrDefault();
                            UserModel.Policies = new string[]
                            {
                                groupMember?.Role ?? string.Empty
                            };
                        }
                    }
                }
            }
        }

        private int _userId;
        public override bool IsAuthorized => _userId > 0;
    }
}
