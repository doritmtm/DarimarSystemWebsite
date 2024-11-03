using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class DarimarSystemDialog : DarimarSystemComponent
    {
        [Inject]
        public required IDialogService DialogService { get; set; }

        public DialogOptions? DialogOptions { get; set; }

        [CascadingParameter]
        public MudDialogInstance? DialogInstance { get; set; }

        public async Task OpenDialog()
        {
            if (DialogOptions != null)
            {
                await DialogService.ShowAsync(GetType(), "", DialogOptions);
            }
            else
            {
                await DialogService.ShowAsync(GetType());
            }
        }

        public void CloseDialog()
        {
            DialogInstance?.Close();
        }
    }
}
