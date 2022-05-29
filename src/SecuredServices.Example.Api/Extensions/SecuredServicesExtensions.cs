using SecuredServices.Core;
using SecuredServices.Core.Protectors;
using SecuredServices.Core.Providers;
using SecuredServices.Example.Api.Models;
using SecuredServices.Example.Api.Roles;
using SecuredServices.Example.Api.Security;

namespace SecuredServices.Example.Api.Extensions
{
    public static class SecuredServicesExtensions
    {
        public static IServiceCollection UseGroupSecuredServices(this IServiceCollection services)
        {
            services.AddScoped<IPolicyProvider, PolicyProvider>(
                        x => new PolicyProvider(typeof(GroupRole)));
            services.AddScoped<ISessionManager, QuerySessionManager>();
            services.AddScoped<IEntityProtector<Group>, EntityProtector<Group>>();
            return services;
        }
    }
}
