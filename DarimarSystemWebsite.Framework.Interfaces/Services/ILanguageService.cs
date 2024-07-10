using DarimarSystemWebsite.Framework.Interfaces.Enums;
using System.Globalization;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface ILanguageService
    {
        public CultureInfo GetCultureInfoForLanguage(LanguageEnum language);
    }
}
