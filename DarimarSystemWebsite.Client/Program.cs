using DarimarSystemWebsite.Framework;
using DarimarSystemWebsite.Framework.Settings;
using DarimarSystemWebsite.Resources;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

StaticSettings.AppAssembly = typeof(Program).Assembly;
StaticSettings.AdditionalAssemblies = [typeof(DarimarSystemWebsite.Client._Imports).Assembly, typeof(DarimarSystemWebsite.Framework._Imports).Assembly];

builder.Services.AddResourceAccess();
builder.Services.AddDarimarSystemServices();

await builder.Build().RunAsync();
