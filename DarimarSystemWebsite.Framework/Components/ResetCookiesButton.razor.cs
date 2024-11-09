using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class ResetCookiesButton : DarimarSystemComponentWithStyle
    {
        public void ResetCookies()
        {
            DarimarSystemService.ResetClientPreferences();
            DarimarSystemService.ServiceHelper?.RegisterAfterRenderAsyncAction(() => { RefreshAll(); return Task.CompletedTask; });
        }
    }
}
