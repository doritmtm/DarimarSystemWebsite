using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

namespace DarimarSystemWebsite.Framework.Services
{
    public class HostInformationService : IHostInformationService
    {
        private IPersistedPreferencesService _persistedPreferencesService;

        private IHostPreRenderingService _hostPreRenderingService;

        public bool IsPreRendering => _hostPreRenderingService.IsPreRendering;

        private HostTypeEnum? _hostType;
        public HostTypeEnum? HostType
        {
            get
            {
                string? value = _persistedPreferencesService.GetPersistedPreference("hostType");
                if (value != null)
                {
                    _hostType = Enum.Parse<HostTypeEnum>(value);
                }

                return _hostType;
            }

            set
            {
                _persistedPreferencesService.PersistPreference("hostType", value?.ToString());
                _hostType = value;
            }
        }

        public HostInformationService(IPersistedPreferencesService persistedPreferencesService, IHostPreRenderingService hostPreRenderingService)
        {
            _persistedPreferencesService = persistedPreferencesService;
            _hostPreRenderingService = hostPreRenderingService;
        }
    }
}
