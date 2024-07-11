using DarimarSystemWebsite.Components;
using DarimarSystemWebsite.Framework;
using DarimarSystemWebsite.Framework.Settings;
using DarimarSystemWebsite.Resources;
using Microsoft.Extensions.Localization;

// Microsoft.Extensions.Localization namespace to look for resources
[assembly: RootNamespace("DarimarSystemWebsite.Resources")]

var builder = WebApplication.CreateBuilder(args);

StaticSettings.AppAssembly = typeof(Program).Assembly;
StaticSettings.AdditionalAssemblies = [typeof(DarimarSystemWebsite.Client._Imports).Assembly, typeof(DarimarSystemWebsite.Framework._Imports).Assembly];
StaticSettings.ResourcesClass = typeof(SiteResources);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDarimarSystemServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DarimarSystemWebsite.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(DarimarSystemWebsite.Framework._Imports).Assembly);

app.Run();
