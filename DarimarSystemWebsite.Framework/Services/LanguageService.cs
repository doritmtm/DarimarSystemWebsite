using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace DarimarSystemWebsite.Framework.Services
{
    public class LanguageService : ILanguageService
    {
        private IServiceHelperComponentHostService _serviceHelperComponentHostService;

        private IHostInformationService _hostInformationService;

        private IPersistedPreferencesService _persistedPreferencesService;

        private ResourceManager _resourceManager;

        private Dictionary<LanguageEnum, Dictionary<string, string>> _translations = [];

        public LanguageService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostInformationService hostInformationService, IPersistedPreferencesService persistedPreferencesService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostInformationService = hostInformationService;
            _persistedPreferencesService = persistedPreferencesService;

            if (StaticSettings.ResourcesClass != null)
            {
                _resourceManager = new ResourceManager(StaticSettings.ResourcesClass);
            }
            else
            {
                throw new NotSupportedException("StaticSettings.ResourcesClass must be defined");
            }

            _persistedPreferencesService = persistedPreferencesService;
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

        public void InitializeLanguages()
        {
            if (_hostInformationService.IsPreRendering)
            {
                foreach (LanguageEnum language in StaticSettings.SupportedLanguages)
                {
                    _translations[language] = [];

                    ResourceSet? resourceSet = _resourceManager.GetResourceSet(GetCultureInfoForLanguage(language), true, true);
                    if (resourceSet != null)
                    {
                        foreach (DictionaryEntry entry in resourceSet.Cast<DictionaryEntry>())
                        {
                            if (entry.Key != null && entry.Value != null)
                            {
                                if (StaticSettings.ResourcesClass != null)
                                {
                                    string key = entry.Key.ToString() ?? "";
                                    string value = entry.Value.ToString() ?? "";
                                    _persistedPreferencesService.PersistPreference($"{StaticSettings.ResourcesClass.Name}-{language.ToString()}-{key}", value);
                                    _translations[language][key] = value;
                                }
                                else
                                {
                                    throw new NotSupportedException("StaticSettings.ResourcesClass must be defined");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (LanguageEnum language in StaticSettings.SupportedLanguages)
                {
                    _translations[language] = [];

                    ResourceSet? resourceSet = _resourceManager.GetResourceSet(GetCultureInfoForLanguage(LanguageEnum.English), true, true);
                    if (resourceSet != null)
                    {
                        foreach (DictionaryEntry entry in resourceSet.Cast<DictionaryEntry>())
                        {
                            if (entry.Key != null)
                            {
                                if (StaticSettings.ResourcesClass != null)
                                {
                                    string key = entry.Key.ToString() ?? "";
                                    _translations[language][key] = _persistedPreferencesService.GetPersistedPreference($"{StaticSettings.ResourcesClass.Name}-{language.ToString()}-{key}") ?? "";
                                }
                                else
                                {
                                    throw new NotSupportedException("StaticSettings.ResourcesClass must be defined");
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ChangeLanguage(LanguageEnum language)
        {
            CultureInfo culture = GetCultureInfoForLanguage(language);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public string? GetLocalizedString(string nameID, LanguageEnum language)
        {
            if (StaticSettings.ResourcesClass != null)
            {
                return _translations[language].ContainsKey(nameID) ? _translations[language][nameID] : null;
            }
            else
            {
                throw new NotSupportedException("StaticSettings.ResourcesClass must be defined");
            }
        }
    }
}
