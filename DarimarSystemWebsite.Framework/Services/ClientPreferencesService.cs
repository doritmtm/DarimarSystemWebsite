using BitzArt.Blazor.Cookies;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace DarimarSystemWebsite.Framework.Services
{
    public class ClientPreferencesService : IClientPreferencesService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IHostTypeInformationService _hostTypeInformationService;

        private ICookieService _cookieService;

        private IHttpContextAccessor? _httpContextAccessor;

        private PersistentComponentState _persistentComponentState;

        private Dictionary<string, string?> _clientPreferences = [];

        private bool? IsPreRendering => !_httpContextAccessor?.HttpContext?.WebSockets?.IsWebSocketRequest;

        public ClientPreferencesService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostTypeInformationService hostTypeInformationService, ICookieService cookieService, PersistentComponentState persistentComponentState)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostTypeInformationService = hostTypeInformationService;
            _cookieService = cookieService;
            _persistentComponentState = persistentComponentState;
        }

        public ClientPreferencesService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostTypeInformationService hostTypeInformationService, ICookieService cookieService, IHttpContextAccessor httpContextAccessor, PersistentComponentState persistentComponentState)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostTypeInformationService = hostTypeInformationService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
            _persistentComponentState = persistentComponentState;
        }

        public string? GetPreference(string name)
        {
            return GetPreferenceAsync(name).Result;
        }

        public void SetPreference(string name, string value)
        {
            SetPreferenceAsync(name, value).Wait();
        }

        public async Task<string?> GetPreferenceAsync(string name)
        {
            if (IsPreRendering == true)
            {
                Cookie? cookie = await _cookieService.GetAsync(name);
                string? value = cookie?.Value;
                _persistentComponentState.RegisterOnPersisting(() =>
                {
                    _persistentComponentState.PersistAsJson(name, value);
                    return Task.CompletedTask;
                }, StaticSettings.GlobalRenderMode);
                return value;
            }
            else
            {
                if (_hostTypeInformationService.HostType == HostTypeEnum.Server)
                {
                    if (_persistentComponentState.TryTakeFromJson(name, out string? value))
                    {
                        _clientPreferences[name] = value;
                    }
                }
                if (_hostTypeInformationService.HostType == HostTypeEnum.WebAssembly)
                {
                    Cookie? cookie = await _cookieService.GetAsync(name);
                    string? value = cookie?.Value;
                    _clientPreferences[name] = value;
                }
                return _clientPreferences[name];
            }
        }

        public Task SetPreferenceAsync(string name, string value)
        {
            _serviceHelperComponentHostService.ServiceHelper?.RegisterAfterRenderAsyncAction(async () =>
            {
                await _cookieService.SetAsync(name, value, DateTimeOffset.Now.AddDays(400));
            });
            _clientPreferences[name] = value;
            return Task.CompletedTask;
        }
    }
}
