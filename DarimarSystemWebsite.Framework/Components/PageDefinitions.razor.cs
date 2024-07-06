using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class PageDefinitions : DarimarSystemComponent
    {
        public PreRenderingContent? PreRenderingContent { get; set; }
        public PageContent? PageContent { get; set; }
    }
}
