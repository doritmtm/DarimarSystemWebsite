using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace DarimarSystemWebsite.Framework.Services
{
    public class LanguageService : ILanguageService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IStringLocalizerFactory _stringLocalizerFactory;

        private IStringLocalizer _stringLocalizer;

        public LanguageService(IServiceHelperComponentHostService serviceHelperComponentHostService, IStringLocalizerFactory stringLocalizerFactory)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _stringLocalizerFactory = stringLocalizerFactory;
            if (StaticSettings.ResourcesClass != null)
            {
                _stringLocalizer = _stringLocalizerFactory.Create(StaticSettings.ResourcesClass);
            }
            else
            {
                throw new NotSupportedException("StaticSettings.ResourcesClass must be defined");
            }
        }

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

        public void ChangeLanguage(LanguageEnum language)
        {
            CultureInfo culture = GetCultureInfoForLanguage(language);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        public string GetLocalizedString(string nameID)
        {
            return _stringLocalizer[nameID];
        }
    }
}
