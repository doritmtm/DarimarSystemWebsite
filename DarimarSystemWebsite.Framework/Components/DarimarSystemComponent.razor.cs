using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemComponent : ComponentBase, IDarimarSystemComponent
    {
        [Inject]
        public required IDarimarSystemService DarimarSystemService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private HttpContext? _httpContext { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public bool IsPreRendering { get { return (_httpContext != null); } }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DarimarSystemService.DarimarSystemComponents.Enqueue(this);
        }

        public virtual bool ReadyToRender()
        {
            return true;
        }

        public virtual void Update()
        {
            InvokeAsync(StateHasChanged);
        }

        public string GetCurrentHref()
        {
            string currentHref = new Uri(NavigationManager.Uri).AbsolutePath;
            return String.IsNullOrEmpty(currentHref) ? "/" : currentHref;
        }
    }
}
