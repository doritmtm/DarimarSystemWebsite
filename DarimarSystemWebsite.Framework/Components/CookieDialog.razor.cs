namespace DarimarSystemWebsite.Framework.Components
{
    public partial class CookieDialog : DarimarSystemDialog
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DialogOptions = new()
            {
                MaxWidth = MudBlazor.MaxWidth.ExtraExtraLarge,
                Position = MudBlazor.DialogPosition.BottomCenter,
            };
        }

        public async Task AllowCookies()
        {
            DarimarSystemService.CookieUserConsent = true;
            await DarimarSystemService.ChangeLanguageAsync(DarimarSystemService.CurrentLanguage);
            await CloseDialog();
        }

        public void DenyCookies()
        {
            DarimarSystemService.CookieUserConsent = false;
            CloseDialog();
        }
    }
}
