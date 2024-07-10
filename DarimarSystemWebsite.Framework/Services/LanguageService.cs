using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using System.Globalization;

namespace DarimarSystemWebsite.Framework.Services
{
    public class LanguageService : ILanguageService
    {
        public CultureInfo GetCultureInfoForLanguage(LanguageEnum language)
        {
            switch (language)
            {
                case LanguageEnum.English:
                    return new CultureInfo("en-US");
                case LanguageEnum.Romana:
                    return new CultureInfo("ro-RO");
            }

            return new CultureInfo("en-US");
        }
    }
}
