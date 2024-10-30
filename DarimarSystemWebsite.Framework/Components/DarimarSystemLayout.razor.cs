using DarimarSystemWebsite.Framework.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemLayout : LayoutComponentBase
    {
        [Inject]
        public required IDarimarSystemService DarimarSystemService { get; set; }

        [Inject]
        public required PersistentComponentState State { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DarimarSystemService.InitializePreferences(State);
        }
    }
}
