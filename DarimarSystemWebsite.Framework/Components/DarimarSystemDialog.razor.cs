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

        public Action? OnOpenAction { get; set; }
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
                OpenedDialog.OnOpenAction = OnOpenAction;
                OpenedDialog.OnCloseAction = OnCloseAction;
            }

            OnOpenAction?.Invoke();
        }

        public Task CloseDialog()
        {
            DialogInstance?.Close();
            OnCloseAction?.Invoke();

            return Task.CompletedTask;
        }

        public async Task CloseDialogAndReopen()
        {
            await CloseDialog();
            await OpenDialog();
        }
    }
}
