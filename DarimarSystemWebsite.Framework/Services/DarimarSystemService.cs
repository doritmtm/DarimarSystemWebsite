using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class DarimarSystemService : IDarimarSystemService
    {
        private ILanguageService _languageService;

        public LanguageEnum CurrentLanguage { get; set; } = LanguageEnum.Romana;

        public DarimarSystemService(ILanguageService languageService)
        {
            _languageService = languageService;
            _languageService.ChangeLanguage(CurrentLanguage);
        }

        public void ChangeLanguage(LanguageEnum language)
        {
            CurrentLanguage = language;
            _languageService.ChangeLanguage(language);
        }

        public string? GetLocalizedString(string nameID)
        {
            return _languageService.GetLocalizedString(nameID);
        }
    }
}
