using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Services
{
    public class DarimarSystemService : IDarimarSystemService
    {
        private ILanguageService _languageService;

        public LanguageEnum CurrentLanguage { get; set; } = LanguageEnum.Romana;

        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; } = [];

        public DarimarSystemService(ILanguageService languageService)
        {
            _languageService = languageService;
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

        public void UpdateAllDarimarSystemComponents()
        {
            foreach (var component in DarimarSystemComponents)
            {
                component.Update();
            }
        }
    }
}
