using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class DarimarSystemService : IDarimarSystemService
    {
        private ILanguageService _languageService;

        private LanguageEnum _currentLanguage = LanguageEnum.Romana;

        public DarimarSystemService(ILanguageService languageService)
        {
            _languageService = languageService;
            _languageService.ChangeLanguage(_currentLanguage);
        }

        public void ChangeLanguage(LanguageEnum language)
        {
            _currentLanguage = language;
            _languageService.ChangeLanguage(language);
        }

        public string? GetLocalizedString(string nameID)
        {
            return _languageService.GetLocalizedString(nameID);
        }
    }
}
