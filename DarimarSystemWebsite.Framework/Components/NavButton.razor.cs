using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class NavButton : DarimarSystemComponent
    {
        [Parameter]
        public string Href { get; set; } = "";
        [Parameter]
        public string Class { get; set; } = "";
    }
}
