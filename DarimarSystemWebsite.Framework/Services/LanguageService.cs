using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Resources;
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

        private ResourceManager _resourceManager, _frameworkResourceManager;

        public LanguageService(IServiceHelperComponentHostService serviceHelperComponentHostService, IHostInformationService hostInformationService, IPersistedPreferencesService persistedPreferencesService)
        {
            _serviceHelperComponentHostService = serviceHelperComponentHostService;
            _hostInformationService = hostInformationService;
            _persistedPreferencesService = persistedPreferencesService;

            if (StaticSettings.ResourcesClass != null)
            {
                _resourceManager = new ResourceManager(StaticSettings.ResourcesClass);
                _frameworkResourceManager = new ResourceManager(typeof(FrameworkResources));
            }
            else
            {
                throw new NotSupportedException("LanguageService: StaticSettings.ResourcesClass must be defined");
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
                                }
                                else
                                {
                                    throw new NotSupportedException("LanguageService: StaticSettings.ResourcesClass must be defined");
                                }
                            }
                        }
                    }

                    ResourceSet? frameworkResourceSet = _frameworkResourceManager.GetResourceSet(GetCultureInfoForLanguage(language), true, true);
                    if (frameworkResourceSet != null)
                    {
                        foreach (DictionaryEntry entry in frameworkResourceSet.Cast<DictionaryEntry>())
                        {
                            if (entry.Key != null && entry.Value != null)
                            {
                                string key = entry.Key.ToString() ?? "";
                                string value = entry.Value.ToString() ?? "";
                                _persistedPreferencesService.PersistPreference($"{typeof(FrameworkResources).Name}-{language.ToString()}-{key}", value);
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
                string? localizedString = _persistedPreferencesService.GetPersistedPreference($"{typeof(FrameworkResources).Name}-{language.ToString()}-{nameID}");
                localizedString ??= _persistedPreferencesService.GetPersistedPreference($"{StaticSettings.ResourcesClass.Name}-{language.ToString()}-{nameID}");
                return localizedString;
            }
            else
            {
                throw new NotSupportedException("LanguageService: StaticSettings.ResourcesClass must be defined");
            }
        }
    }
}
