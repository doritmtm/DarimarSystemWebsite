using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemComponent : ComponentBase
    {
        [CascadingParameter]
        private HttpContext? _httpContext { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public bool IsPreRendering { get { return (_httpContext != null); } }

        public virtual bool ReadyToRender()
        {
            return true;
        }

        public virtual void Update()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
