using BitzArt.Blazor.Cookies;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class ClientPreferencesService : IClientPreferencesService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IHostInformationService _hostInformationService;

        private IPersistedPreferencesService _persistedPreferencesService;

        private ICookieService _cookieService;

        private List<string> _clientPreferencesKeys = [];

        private bool? _cookieUserConsent;
        public bool? CookieUserConsent
        {
            get
            {
                return _cookieUserConsent;
            }
            set
            {
                _cookieUserConsent = value;
                if (value != null)
                {
                    SetPreferenceAsync("cookieUserConsent", value.ToString()!);
                }
            }
        }

        public ClientPreferencesService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostInformationService hostInformationService, IPersistedPreferencesService persistedPreferencesService, ICookieService cookieService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostInformationService = hostInformationService;
            _persistedPreferencesService = persistedPreferencesService;
            _cookieService = cookieService;
        }

        public async Task Initialize()
        {
            _clientPreferencesKeys = _persistedPreferencesService.GetPersistedObject<List<string>>("clientPreferencesKeys") ?? [];
            string? cookieUserConsentCookie = await GetPreferenceAsync("cookieUserConsent");
            if (cookieUserConsentCookie != null)
            {
                _cookieUserConsent = bool.Parse(cookieUserConsentCookie);
            }
        }

        public async Task<string?> GetPreferenceAsync(string name)
        {
            if (CookieUserConsent == true || name == "cookieUserConsent")
            {
                if (_hostInformationService.IsPreRendering)
                {
                    Cookie? cookie = await _cookieService.GetAsync(name);
                    string? value = cookie?.Value;
                    _persistedPreferencesService.PersistPreference(name, value);

                    if (!_clientPreferencesKeys.Contains(name))
                    {
                        _clientPreferencesKeys.Add(name);
                        _persistedPreferencesService.PersistObject("clientPreferencesKeys", _clientPreferencesKeys);
                    }

                    return value;
                }
                else
                {
                    return _persistedPreferencesService.GetPersistedPreference(name);
                }
            }

            return null;
        }

        public Task SetPreferenceAsync(string name, string value)
        {
            if (CookieUserConsent == true || name == "cookieUserConsent")
            {
                _serviceHelperComponentHostService.ServiceHelper?.RegisterAfterRenderAsyncAction(async () =>
                {
                    await _cookieService.SetAsync(name, value, DateTimeOffset.Now.AddDays(400));
                });
            }

            _persistedPreferencesService.PersistPreference(name, value);

            if (!_clientPreferencesKeys.Contains(name))
            {
                _clientPreferencesKeys.Add(name);
                _persistedPreferencesService.PersistObject("clientPreferencesKeys", _clientPreferencesKeys);
            }

            return Task.CompletedTask;
        }

        public void ResetAllPreferences()
        {
            _serviceHelperComponentHostService.ServiceHelper?.RegisterAfterRenderAsyncAction(async () =>
            {
                foreach (string key in _clientPreferencesKeys)
                {
                    await _cookieService.RemoveAsync(key);
                    _persistedPreferencesService.RemovePersistedPreference(key);
                }

                _clientPreferencesKeys.Clear();
                _persistedPreferencesService.RemovePersistedObject("clientPreferencesKeys");
            });
        }
    }
}
