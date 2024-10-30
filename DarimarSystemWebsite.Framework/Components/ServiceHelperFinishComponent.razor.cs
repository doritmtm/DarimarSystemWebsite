namespace DarimarSystemWebsite.Framework.Components
{
    public partial class ServiceHelperFinishComponent : DarimarSystemComponent
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DarimarSystemService.ServiceHelper?.RunOnFinishActions();
        }
    }
}
