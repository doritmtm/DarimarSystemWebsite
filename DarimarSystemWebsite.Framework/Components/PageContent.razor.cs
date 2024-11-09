using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class PageContent : DarimarSystemComponent
    {
        [CascadingParameter]
        public PageDefinitions? PageDefinitions { get; set; }

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public MaxWidth? MaxWidth { get; set; }

        [Parameter]
        public string? Style { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (PageDefinitions != null)
            {
                PageDefinitions.PageContent = this;
            }
        }
    }
}
