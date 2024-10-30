using DarimarSystemWebsite.Framework.Interfaces.Components;
using Microsoft.JSInterop;
using System.Collections.Concurrent;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class ServiceHelperComponent : DarimarSystemComponent, IServiceHelperComponent
    {
        private ConcurrentQueue<Action> _afterRenderActions = [];
        private ConcurrentQueue<Func<Task>> _afterRenderAsyncActions = [];
        private ConcurrentQueue<Action> _onFinishActions = [];

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DarimarSystemService.ServiceHelper = this;
            RegisterOnFinishAction(DarimarSystemService.CommitPreferencesToPersistentSystem);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await DarimarSystemService.InitializeLanguageAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            foreach (var action in _afterRenderActions)
            {
                action();
            }

            _afterRenderActions.Clear();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            foreach (var action in _afterRenderAsyncActions)
            {
                await action();
            }

            _afterRenderAsyncActions.Clear();
        }

        public void RegisterAfterRenderAction(Action action)
        {
            _afterRenderActions.Enqueue(action);
            StateHasChanged();
        }

        public void RegisterAfterRenderAsyncAction(Func<Task> asyncAction)
        {
            _afterRenderAsyncActions.Enqueue(asyncAction);
            StateHasChanged();
        }

        public void RegisterOnFinishAction(Action action)
        {
            _onFinishActions.Enqueue(action);
        }

        public void RegisterJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments)
        {
            RegisterAfterRenderAsyncAction(async () => await JSRuntime.InvokeVoidAsync(jsFunctionName, jsArguments));
            StateHasChanged();
        }

        public async Task RunJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments)
        {
            await JSRuntime.InvokeVoidAsync(jsFunctionName, jsArguments);
        }

        public async Task<ReturnType> RunJSInvokeAsyncAction<ReturnType>(string jsFunctionName, params object?[]? jsArguments)
        {
            return await JSRuntime.InvokeAsync<ReturnType>(jsFunctionName, jsArguments);
        }

        public void RunOnFinishActions()
        {
            foreach (var action in _onFinishActions)
            {
                action();
            }

            _onFinishActions.Clear();
        }
    }
}
