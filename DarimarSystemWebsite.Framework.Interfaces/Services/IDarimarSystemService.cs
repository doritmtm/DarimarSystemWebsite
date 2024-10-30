using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IDarimarSystemService
    {
        public IServiceHelperComponent? ServiceHelper { get; set; }
        public HostTypeEnum? HostType { get; set; }
        public LanguageEnum CurrentLanguage { get; }
        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; }
        public void InitializePreferences(object state);
        public Task InitializeLanguageAsync();
        public Task ChangeLanguageAsync(LanguageEnum language);
        public string? GetLocalizedString(string nameID);
        public string GetAppVersion();
        public Task<string?> GetClientPreferenceAsync(string name);
        public Task SetClientPreferenceAsync(string name, string value);
        public void CommitPreferencesToPersistentSystem();
        public void UpdateAllDarimarSystemComponents();
    }
}
