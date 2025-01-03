﻿using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IDarimarSystemService
    {
        public IServiceHelperComponent? ServiceHelper { get; set; }
        public HostTypeEnum? HostType { get; set; }
        public LanguageEnum CurrentLanguage { get; }
        public bool? CookieUserConsent { get; set; }
        public IDarimarSystemLayout? DarimarSystemLayout { get; set; }
        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; }
        public void InitializePersistedPreferences(object state);
        public Task InitializeClientPreferences();
        public Task InitializeLanguageAsync();
        public Task ChangeLanguageAsync(LanguageEnum language);
        public string? GetLocalizedString(string nameID);
        public string GetAppVersion();
        public Task<string?> GetClientPreferenceAsync(string name);
        public Task SetClientPreferenceAsync(string name, string value);
        public void ResetClientPreferences();
        public void CommitPreferencesToPersistentSystem();
        public void UpdateAllDarimarSystemComponents();
    }
}
