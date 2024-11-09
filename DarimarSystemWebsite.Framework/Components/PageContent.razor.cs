using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class PageContent : DarimarSystemComponentWithStyle
    {
        [CascadingParameter]
        public PageDefinitions? PageDefinitions { get; set; }

        [Parameter]
        public MaxWidth? MaxWidth { get; set; }

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
