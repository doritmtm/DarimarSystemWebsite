using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DarimarSystemWebsite.Framework
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDarimarSystemServices(this IServiceCollection services)
        {
            services.AddScoped<IDarimarSystemService, DarimarSystemService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddLocalization();

            return services;
        }
    }
}
