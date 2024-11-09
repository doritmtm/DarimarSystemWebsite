using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemLayout : LayoutComponentBase, IDarimarSystemLayout
    {
        [Inject]
        public required IDarimarSystemService DarimarSystemService { get; set; }

        [Inject]
        public required PersistentComponentState State { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DarimarSystemService.DarimarSystemLayout = this;
            DarimarSystemService.InitializePersistedPreferences(State);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await DarimarSystemService.InitializeClientPreferences();
        }

        public virtual void Update()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
