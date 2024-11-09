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

        public DarimarSystemDialog? OpenedDialog { get; set; }

        public Action? OnCloseAction { get; set; }

        public async Task OpenDialog()
        {
            IDialogReference? dialogReference = null;

            if (DialogOptions != null)
            {
                dialogReference = await DialogService.ShowAsync(GetType(), "", DialogOptions);
            }
            else
            {
                dialogReference = await DialogService.ShowAsync(GetType());
            }

            OpenedDialog = dialogReference.Dialog as DarimarSystemDialog;

            if (OpenedDialog != null)
            {
                OpenedDialog.OnCloseAction = OnCloseAction;
            }
        }

        public void CloseDialog()
        {
            DialogInstance?.Close();
            OnCloseAction?.Invoke();
        }
    }
}
