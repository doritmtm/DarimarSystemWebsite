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
        private ILanguageService _languageService;

        private IConfiguration _configurationService;

        public LanguageEnum CurrentLanguage { get; set; } = LanguageEnum.Romana;

        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; } = [];

        public DarimarSystemService(ILanguageService languageService, IConfiguration configurationService)
        {
            _languageService = languageService;
            _configurationService = configurationService;
        }

        public void ChangeLanguage(LanguageEnum language)
        {
            if (StaticSettings.SupportedLanguages.Contains(language))
            {
                CurrentLanguage = language;
                _languageService.ChangeLanguage(language);
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

        public void UpdateAllDarimarSystemComponents()
        {
            foreach (var component in DarimarSystemComponents)
            {
                component.Update();
            }
        }
    }
}
