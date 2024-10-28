using DarimarSystemWebsite.Framework.Interfaces.Components;
using Microsoft.JSInterop;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class ServiceHelperComponent : DarimarSystemComponent, IServiceHelperComponent
    {
        private Queue<Action> _afterRenderActions = [];
        private Queue<Func<Task>> _afterRenderAsyncActions = [];

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DarimarSystemService.ServiceHelper = this;
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

        public void RegisterJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments)
        {
            RegisterAfterRenderAsyncAction(async () => await JSRuntime.InvokeVoidAsync(jsFunctionName, jsArguments));
            StateHasChanged();
        }

        public void RunJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments)
        {
            JSRuntime.InvokeVoidAsync(jsFunctionName, jsArguments);
        }

        public ReturnType RunJSInvokeAsyncAction<ReturnType>(string jsFunctionName, params object?[]? jsArguments)
        {
            return JSRuntime.InvokeAsync<ReturnType>(jsFunctionName, jsArguments).Result;
        }
    }
}
