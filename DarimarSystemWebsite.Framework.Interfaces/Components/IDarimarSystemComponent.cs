namespace DarimarSystemWebsite.Framework.Interfaces.Components
{
    public interface IDarimarSystemComponent
    {
        public bool ReadyToRender();
        public void Update();
        public string GetCurrentHref();
    }
}
