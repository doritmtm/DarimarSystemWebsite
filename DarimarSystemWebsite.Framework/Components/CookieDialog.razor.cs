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

        public void AllowCookies()
        {
            DarimarSystemService.CookieUserConsent = true;
            CloseDialog();
        }

        public void DenyCookies()
        {
            DarimarSystemService.CookieUserConsent = false;
            CloseDialog();
        }
    }
}
