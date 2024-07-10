using DarimarSystemWebsite.Framework.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DarimarSystemWebsite.Resources
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddResourceAccess(this IServiceCollection services)
        {
            services.AddScoped<IResourceAccess, ResourceAccess>();

            return services;
        }
    }
}
