using DarimarSystemWebsite.Framework.Interfaces.Enums;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IHostInformationService
    {
        public HostTypeEnum? HostType { get; set; }
        public bool IsPreRendering { get; }
    }
}
