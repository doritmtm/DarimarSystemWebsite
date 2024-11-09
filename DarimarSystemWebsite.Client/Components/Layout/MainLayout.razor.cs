using DarimarSystemWebsite.Framework.Components;
using MudBlazor;

namespace DarimarSystemWebsite.Client.Components.Layout
{
    public partial class MainLayout : DarimarSystemLayout
    {
        private bool _isDarkMode = true;

        private MudTheme? _theme = null;

        public CookieDialog? CookieDialog { get; set; }

        private bool _isCookieDialogOpened = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _theme = new()
            {
                PaletteLight = _lightPalette,
                PaletteDark = _darkPalette,
                LayoutProperties = new LayoutProperties()
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _isDarkMode = bool.Parse(await DarimarSystemService.GetClientPreferenceAsync("darkMode") ?? "true");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (CookieDialog != null && !_isCookieDialogOpened)
            {
                if (DarimarSystemService.CookieUserConsent == null)
                {
                    CookieDialog.OnOpenAction = () => { _isCookieDialogOpened = true; };
                    CookieDialog.OnCloseAction = () => { _isCookieDialogOpened = false; };
                    await CookieDialog.OpenDialog();
                }
            }
        }

        private async Task DarkModeToggle()
        {
            _isDarkMode = !_isDarkMode;

            await DarimarSystemService.SetClientPreferenceAsync("darkMode", _isDarkMode.ToString());
        }

        private readonly PaletteLight _lightPalette = new()
        {
            Primary = "#0a5ef0",
            Black = "#110e2d",
            AppbarText = "#424242",
            AppbarBackground = "rgba(255,255,255,0.8)",
            DrawerBackground = "#ffffff",
            GrayLight = "#e8e8e8",
            GrayLighter = "#f9f9f9",
        };

        private readonly PaletteDark _darkPalette = new()
        {
            Primary = "#0f77f7",
            Surface = "#1e1e2d",
            Background = "#1a1a27",
            BackgroundGray = "#151521",
            AppbarText = "#92929f",
            AppbarBackground = "rgba(26,26,39,0.8)",
            DrawerBackground = "#1a1a27",
            ActionDefault = "#74718e",
            ActionDisabled = "#9999994d",
            ActionDisabledBackground = "#605f6d4d",
            TextPrimary = "#b2b0bf",
            TextSecondary = "#92929f",
            TextDisabled = "#ffffff33",
            DrawerIcon = "#92929f",
            DrawerText = "#92929f",
            GrayLight = "#2a2833",
            GrayLighter = "#1e1e2d",
            Info = "#4a86ff",
            Success = "#3dcb6c",
            Warning = "#ffb545",
            Error = "#ff3f5f",
            LinesDefault = "#33323e",
            TableLines = "#33323e",
            Divider = "#292838",
            OverlayLight = "#1e1e2d80",
        };

        public string DarkLightModeButtonIcon => _isDarkMode switch
        {
            true => Icons.Material.Rounded.AutoMode,
            false => Icons.Material.Outlined.DarkMode,
        };
    }
}
