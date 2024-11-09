using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class NavButton : DarimarSystemComponentWithStyle
    {
        [Parameter]
        public string Href { get; set; } = "";
    }
}
