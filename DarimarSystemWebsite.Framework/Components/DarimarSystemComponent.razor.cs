using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemComponent : ComponentBase
    {
        [CascadingParameter]
        private HttpContext? _httpContext { get; set; }

        public bool IsPreRendering { get { return (_httpContext != null); } }

        public virtual bool ReadyToRender()
        {
            return !IsPreRendering;
        }

        public virtual void Update()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
