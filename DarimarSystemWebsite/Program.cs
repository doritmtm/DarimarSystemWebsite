using BitzArt.Blazor.Cookies;
using DarimarSystemWebsite.Client;
using DarimarSystemWebsite.Components;
using DarimarSystemWebsite.Framework;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Settings;
using DarimarSystemWebsite.Resources;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

// Microsoft.Extensions.Localization namespace to look for resources
[assembly: RootNamespace("DarimarSystemWebsite.Resources")]

var builder = WebApplication.CreateBuilder(args);

StaticSettings.AppAssembly = typeof(Program).Assembly;
StaticSettings.AdditionalAssemblies = [typeof(DarimarSystemWebsite.Client._Imports).Assembly, typeof(DarimarSystemWebsite.Framework._Imports).Assembly, typeof(DarimarSystemWebsite.Framework.Resources.FrameworkResources).Assembly];
StaticSettings.ResourcesClass = typeof(SiteResources);
StaticSettings.SupportedLanguages.Add(LanguageEnum.English);
StaticSettings.SupportedLanguages.Add(LanguageEnum.Romana);
StaticSettings.GlobalRenderMode = RenderMode.InteractiveServer;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();

builder.AddBlazorCookies();
builder.Services.AddDarimarSystemServices();
builder.Services.AddScoped<IServerApp, ServerApp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    //TODO: Error page
    //app.UseExceptionHandler("/Error", createScopeForErrors: true);
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
    .AddAdditionalAssemblies(typeof(DarimarSystemWebsite.Framework._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(DarimarSystemWebsite.Framework.Resources.FrameworkResources).Assembly);

app.Run();
