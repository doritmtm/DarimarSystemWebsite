using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class DarimarSystemService : IDarimarSystemService
    {
        private IResourceAccess _resourceAccess;
        private ILanguageService _languageService;

        public DarimarSystemService(IResourceAccess resourceAccess,
                                    ILanguageService languageService)
        {
            _resourceAccess = resourceAccess;
            _languageService = languageService;
        }

        private LanguageEnum _currentLanguage = LanguageEnum.Romana;

        public void ChangeLanguage(LanguageEnum language)
        {
            _currentLanguage = language;
        }

        public string? GetLocalizedString(string nameID)
        {
            return _resourceAccess.GetLocalizedString(nameID, _languageService.GetCultureInfoForLanguage(_currentLanguage));
        }
    }
}
