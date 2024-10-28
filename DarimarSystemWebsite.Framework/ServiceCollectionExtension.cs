﻿using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Services;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DarimarSystemWebsite.Framework
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDarimarSystemServices(this IServiceCollection services)
        {
            // Add MudBlazor services
            services.AddMudServices();

            services.AddScoped<IServiceHelperComponentHostService, ServiceHelperComponentHostService>();
            services.AddScoped<IHostTypeInformationService, HostTypeInformationService>();
            services.AddScoped<IDarimarSystemService, DarimarSystemService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IClientPreferencesService, ClientPreferencesService>();
            services.AddLocalization();

            return services;
        }
    }
}
