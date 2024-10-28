using DarimarSystemWebsite.Framework.Interfaces.Enums;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IHostTypeInformationService
    {
        public HostTypeEnum? HostType { get; set; }
    }
}
