using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Settings;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class LanguageSwitcher : DarimarSystemComponent
    {
        public LanguageEnum? GetTheOtherLanguage()
        {
            return StaticSettings.SupportedLanguages.Where((lang) => lang != DarimarSystemService.CurrentLanguage).First();
        }

        public async Task SwitchLanguage()
        {
            await DarimarSystemService.ChangeLanguageAsync(GetTheOtherLanguage()!.Value);
            DarimarSystemService.UpdateAllDarimarSystemComponents();
        }
    }
}
