using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Enums;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IDarimarSystemService
    {
        public LanguageEnum CurrentLanguage { get; set; }
        public ConcurrentQueue<IDarimarSystemComponent> DarimarSystemComponents { get; set; }
        public void ChangeLanguage(LanguageEnum language);
        public string? GetLocalizedString(string nameID);
        public string GetAppVersion();
        public void UpdateAllDarimarSystemComponents();
    }
}
