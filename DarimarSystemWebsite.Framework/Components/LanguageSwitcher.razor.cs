using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class LanguageSwitcher : DarimarSystemComponentWithStyle
    {
        [Parameter]
        public Func<Task>? OnClick { get; set; }

        public LanguageEnum? GetTheOtherLanguage()
        {
            return StaticSettings.SupportedLanguages.Where((lang) => lang != DarimarSystemService.CurrentLanguage).First();
        }

        public async Task SwitchLanguage()
        {
            await DarimarSystemService.ChangeLanguageAsync(GetTheOtherLanguage()!.Value);
            DarimarSystemService.UpdateAllDarimarSystemComponents();

            if (OnClick != null)
            {
                await OnClick.Invoke();
            }
        }
    }
}
