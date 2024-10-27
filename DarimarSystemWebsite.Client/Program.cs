using DarimarSystemWebsite.Client;
using DarimarSystemWebsite.Framework;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Settings;
using DarimarSystemWebsite.Resources;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;

// Microsoft.Extensions.Localization namespace to look for resources
[assembly: RootNamespace("DarimarSystemWebsite.Resources")]

var builder = WebAssemblyHostBuilder.CreateDefault(args);

StaticSettings.AppAssembly = typeof(Program).Assembly;
StaticSettings.AdditionalAssemblies = [typeof(DarimarSystemWebsite.Client._Imports).Assembly, typeof(DarimarSystemWebsite.Framework._Imports).Assembly];
StaticSettings.ResourcesClass = typeof(SiteResources);
StaticSettings.SupportedLanguages.Add(LanguageEnum.English);
StaticSettings.SupportedLanguages.Add(LanguageEnum.Romana);
StaticSettings.GlobalRenderMode = RenderMode.InteractiveWebAssembly;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDarimarSystemServices();
builder.Services.AddScoped<IWasmApp, WasmApp>();

var app = builder.Build();

IWasmApp? wasmApp = app.Services.GetService<IWasmApp>();
wasmApp?.Initialize();

await app.RunAsync();
