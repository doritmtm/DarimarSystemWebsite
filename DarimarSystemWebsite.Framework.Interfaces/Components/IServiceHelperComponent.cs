namespace DarimarSystemWebsite.Framework.Interfaces.Components
{
    public interface IServiceHelperComponent
    {
        public void RegisterAfterRenderAction(Action action);
        public void RegisterAfterRenderAsyncAction(Func<Task> asyncAction);
        public void RegisterJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments);
        public Task RunJSInvokeVoidAsyncAction(string jsFunctionName, params object?[]? jsArguments);
        public Task<ReturnType> RunJSInvokeAsyncAction<ReturnType>(string jsFunctionName, params object?[]? jsArguments);
    }
}
