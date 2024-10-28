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

        private IHostTypeInformationService _hostTypeInformationService;

        private ILanguageService _languageService;

        private IConfiguration _configurationService;

        private IClientPreferencesService _clientPreferencesService;

        public IServiceHelperComponent? ServiceHelper
        {
            get => _serviceHelperComponentHostService.ServiceHelper;
            set => _serviceHelperComponentHostService.ServiceHelper = value;
        }

        public HostTypeEnum? HostType
        {
            get => _hostTypeInformationService.HostType;
            set => _hostTypeInformationService.HostType = value;
        }

        private LanguageEnum _defaultLanguage = LanguageEnum.Romana;
        private LanguageEnum _currentLanguage;
        public LanguageEnum CurrentLanguage => _currentLanguage;

        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; } = [];

        public DarimarSystemService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostTypeInformationService hostTypeInformationService, ILanguageService languageService, IConfiguration configurationService, IClientPreferencesService clientPreferencesService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostTypeInformationService = hostTypeInformationService;
            _languageService = languageService;
            _configurationService = configurationService;
            _clientPreferencesService = clientPreferencesService;
            _currentLanguage = _defaultLanguage;
        }

        public void InitializeLanguage()
        {
            LanguageEnum languagePreference = Enum.Parse<LanguageEnum>(_clientPreferencesService.GetPreference("language") ?? _defaultLanguage.ToString());
            ChangeLanguage(languagePreference);
        }

        public async Task InitializeLanguageAsync()
        {
            LanguageEnum languagePreference = Enum.Parse<LanguageEnum>(await _clientPreferencesService.GetPreferenceAsync("language") ?? _defaultLanguage.ToString());
            ChangeLanguage(languagePreference);
        }

        public void ChangeLanguage(LanguageEnum language)
        {
            if (StaticSettings.SupportedLanguages.Contains(language))
            {
                _currentLanguage = language;
                _languageService.ChangeLanguage(language);
                _clientPreferencesService.SetPreference("language", language.ToString());
            }
            else
            {
                throw new NotSupportedException("The given language as parameter is not considered as supported in SupportedLanguages");
            }
        }

        public string? GetLocalizedString(string nameID)
        {
            return _languageService.GetLocalizedString(nameID);
        }

        public string GetAppVersion()
        {
            string? version = _configurationService["Version"];

            if (version != null)
            {
                return version;
            }

            throw new NotSupportedException("The version must be defined as Version inside the main app appsettings.json");
        }
        public string? GetPreference(string name)
        {
            return _clientPreferencesService.GetPreference(name);
        }

        public void SetPreference(string name, string value)
        {
            _clientPreferencesService.SetPreference(name, value);
        }

        public Task<string?> GetPreferenceAsync(string name)
        {
            return _clientPreferencesService.GetPreferenceAsync(name);
        }

        public Task SetPreferenceAsync(string name, string value)
        {
            return _clientPreferencesService.SetPreferenceAsync(name, value);
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
