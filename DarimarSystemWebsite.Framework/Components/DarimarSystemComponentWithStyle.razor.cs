using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemComponentWithStyle : DarimarSystemComponent
    {
        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public string? Style { get; set; }
    }
}
