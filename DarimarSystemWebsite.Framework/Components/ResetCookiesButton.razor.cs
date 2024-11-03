using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class ResetCookiesButton : DarimarSystemComponent
    {
        [Parameter]
        public string Class { get; set; } = "";

        public void ResetCookies()
        {
            DarimarSystemService.ResetClientPreferences();
            RefreshAll();
        }
    }
}
