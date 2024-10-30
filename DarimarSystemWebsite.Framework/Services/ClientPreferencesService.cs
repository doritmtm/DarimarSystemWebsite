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
                return _persistedPreferencesService.GetPersistedPreference(name);
            }
        }

        public Task SetPreferenceAsync(string name, string value)
        {
            _serviceHelperComponentHostService.ServiceHelper?.RegisterAfterRenderAsyncAction(async () =>
            {
                await _cookieService.SetAsync(name, value, DateTimeOffset.Now.AddDays(400));
            });
            return Task.CompletedTask;
        }
    }
}
