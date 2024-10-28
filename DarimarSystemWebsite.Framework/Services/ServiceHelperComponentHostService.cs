using DarimarSystemWebsite.Framework.Interfaces.Components;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class ServiceHelperComponentHostService : IServiceHelperComponentHostService
    {
        public IServiceHelperComponent? ServiceHelper { get; set; }
    }
}
