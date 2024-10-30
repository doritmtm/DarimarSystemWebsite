using BitzArt.Blazor.Cookies;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class ClientPreferencesService : IClientPreferencesService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IHostInformationService _hostInformationService;

        private IPersistedPreferencesService _persistedPreferencesService;

        private ICookieService _cookieService;

        private Dictionary<string, string?> _clientPreferences = [];

        public ClientPreferencesService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostInformationService hostInformationService, IPersistedPreferencesService persistedPreferencesService, ICookieService cookieService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostInformationService = hostInformationService;
            _persistedPreferencesService = persistedPreferencesService;
            _cookieService = cookieService;
        }

        public async Task<string?> GetPreferenceAsync(string name)
        {
            if (_hostInformationService.IsPreRendering)
            {
                Cookie? cookie = await _cookieService.GetAsync(name);
                string? value = cookie?.Value;
                _persistedPreferencesService.PersistPreference(name, value);
                return value;
            }
            else
            {
                if (_hostInformationService.HostType == HostTypeEnum.Server)
                {
                    string? value = _persistedPreferencesService.GetPersistedPreference(name);
                    _clientPreferences[name] = value;
                }
                if (_hostInformationService.HostType == HostTypeEnum.WebAssembly)
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
