using DarimarSystemWebsite.Framework;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using DarimarSystemWebsite.Resources;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;

// Microsoft.Extensions.Localization namespace to look for resources
[assembly: RootNamespace("DarimarSystemWebsite.Resources")]

var builder = WebAssemblyHostBuilder.CreateDefault(args);

StaticSettings.AppAssembly = typeof(Program).Assembly;
StaticSettings.AdditionalAssemblies = [typeof(DarimarSystemWebsite.Client._Imports).Assembly, typeof(DarimarSystemWebsite.Framework._Imports).Assembly];
StaticSettings.ResourcesClass = typeof(SiteResources);

builder.Services.AddDarimarSystemServices();

var app = builder.Build();

var darimarSystemService = app.Services.GetRequiredService<IDarimarSystemService>();
darimarSystemService.ChangeLanguage(darimarSystemService.CurrentLanguage);

await app.RunAsync();
