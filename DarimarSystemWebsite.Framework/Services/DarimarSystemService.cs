using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Services
{
    public class DarimarSystemService : IDarimarSystemService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IHostInformationService _hostInformationService;

        private ILanguageService _languageService;

        private IConfiguration _configurationService;

        private IClientPreferencesService _clientPreferencesService;

        private IPersistedPreferencesService _persistedPreferencesService;

        public IServiceHelperComponent? ServiceHelper
        {
            get => _serviceHelperComponentHostService.ServiceHelper;
            set => _serviceHelperComponentHostService.ServiceHelper = value;
        }

        public HostTypeEnum? HostType
        {
            get => _hostInformationService.HostType;
            set => _hostInformationService.HostType = value;
        }

        private LanguageEnum _defaultLanguage = LanguageEnum.Romana;
        private LanguageEnum _currentLanguage;
        public LanguageEnum CurrentLanguage => _currentLanguage;

        public bool? CookieUserConsent
        {
            get => _clientPreferencesService.CookieUserConsent;
            set => _clientPreferencesService.CookieUserConsent = value;
        }

        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; } = [];

        public DarimarSystemService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostInformationService hostInformationService, ILanguageService languageService, IConfiguration configurationService, IClientPreferencesService clientPreferencesService, IPersistedPreferencesService persistedPreferencesService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostInformationService = hostInformationService;
            _languageService = languageService;
            _configurationService = configurationService;
            _clientPreferencesService = clientPreferencesService;
            _persistedPreferencesService = persistedPreferencesService;
            _currentLanguage = _defaultLanguage;
        }

        public void InitializePersistedPreferences(object state)
        {
            _persistedPreferencesService.InitializePreferences(state);
        }

        public async Task InitializeClientPreferences()
        {
            await _clientPreferencesService.Initialize();
        }

        public async Task InitializeLanguageAsync()
        {
            _languageService.InitializeLanguages();
            LanguageEnum languagePreference = Enum.Parse<LanguageEnum>(await _clientPreferencesService.GetPreferenceAsync("language") ?? _defaultLanguage.ToString());
            await ChangeLanguageAsync(languagePreference);
        }

        public async Task ChangeLanguageAsync(LanguageEnum language)
        {
            if (StaticSettings.SupportedLanguages.Contains(language))
            {
                _currentLanguage = language;
                _languageService.ChangeLanguage(language);
                await _clientPreferencesService.SetPreferenceAsync("language", language.ToString());
            }
            else
            {
                throw new NotSupportedException("DarimarSystemService: The given language as parameter is not considered as supported in SupportedLanguages");
            }
        }

        public string? GetLocalizedString(string nameID)
        {
            return _languageService.GetLocalizedString(nameID, _currentLanguage);
        }

        public string GetAppVersion()
        {
            string? version = _configurationService["Version"];

            if (version != null)
            {
                return version;
            }

            throw new NotSupportedException("DarimarSystemService: The version must be defined as Version inside the main app appsettings.json");
        }

        public Task<string?> GetClientPreferenceAsync(string name)
        {
            return _clientPreferencesService.GetPreferenceAsync(name);
        }

        public Task SetClientPreferenceAsync(string name, string value)
        {
            return _clientPreferencesService.SetPreferenceAsync(name, value);
        }

        public void ResetClientPreferences()
        {
            _clientPreferencesService.ResetAllPreferences();
        }

        public void CommitPreferencesToPersistentSystem()
        {
            _persistedPreferencesService.CommitToPersistingSystem();
        }

        public void UpdateAllDarimarSystemComponents()
        {
            foreach (var component in DarimarSystemComponents)
            {
                component.Update();
            }
        }
    }
}
