using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class RenderPageContent : DarimarSystemComponent
    {
        [CascadingParameter]
        public PageDefinitions? PageDefinitions { get; set; }
    }
}
