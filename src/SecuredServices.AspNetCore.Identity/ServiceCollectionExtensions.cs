using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SecuredServices.AspNetCore.Identity.Sessions;
using SecuredServices.Core;
using SecuredServices.Core.Providers;

namespace SecuredServices.AspNetCore.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseEntityProtector<TIdentity, TRole>(this IServiceCollection services)
            where TIdentity : IUser
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPolicyProvider, PolicyProvider>(
                        x => new PolicyProvider(typeof(TRole)));
            services.AddTransient<ISessionManager, IdentitySessionManager<TIdentity>>();
            return services;
        }
    }
}