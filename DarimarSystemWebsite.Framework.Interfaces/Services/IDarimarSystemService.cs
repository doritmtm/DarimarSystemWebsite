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
        public void InitializeLanguage();
        public Task InitializeLanguageAsync();
        public void ChangeLanguage(LanguageEnum language);
        public string? GetLocalizedString(string nameID);
        public string GetAppVersion();
        public string? GetPreference(string name);
        public void SetPreference(string name, string value);
        public Task<string?> GetPreferenceAsync(string name);
        public Task SetPreferenceAsync(string name, string value);
        public void UpdateAllDarimarSystemComponents();
    }
}
