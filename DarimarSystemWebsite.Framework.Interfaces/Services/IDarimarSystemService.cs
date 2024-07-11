using DarimarSystemWebsite.Framework.Interfaces.Enums;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IDarimarSystemService
    {
        public LanguageEnum CurrentLanguage { get; set; }
        public void ChangeLanguage(LanguageEnum language);
        public string? GetLocalizedString(string nameID);
    }
}
