using DarimarSystemWebsite.Framework.Interfaces.Enums;
using System.Globalization;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface ILanguageService
    {
        public CultureInfo GetCultureInfoForLanguage(LanguageEnum language);
        public void InitializeLanguages();
        public void ChangeLanguage(LanguageEnum language);
        public string? GetLocalizedString(string nameID, LanguageEnum language);
    }
}
