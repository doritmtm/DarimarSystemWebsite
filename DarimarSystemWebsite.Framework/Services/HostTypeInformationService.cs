using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace DarimarSystemWebsite.Framework.Services
{
    public class HostTypeInformationService : IHostTypeInformationService
    {
        private PersistentComponentState _persistentComponentState;

        private IHttpContextAccessor? _httpContextAccessor;

        private bool? IsPreRendering => !_httpContextAccessor?.HttpContext?.WebSockets?.IsWebSocketRequest;

        private HostTypeEnum? _hostType;
        public HostTypeEnum? HostType
        {
            get
            {
                if (IsPreRendering != true)
                {
                    if (_persistentComponentState.TryTakeFromJson("hostType", out string? value))
                    {
                        if (value != null)
                        {
                            _hostType = Enum.Parse<HostTypeEnum>(value);
                        }
                    }
                }

                return _hostType;
            }

            set
            {
                if (IsPreRendering == true)
                {
                    _persistentComponentState.RegisterOnPersisting(() =>
                    {
                        _persistentComponentState.PersistAsJson("hostType", value.ToString());
                        return Task.CompletedTask;
                    }, StaticSettings.GlobalRenderMode);
                }
                _hostType = value;
            }
        }

        public HostTypeInformationService(PersistentComponentState persistentComponentState)
        {
            _persistentComponentState = persistentComponentState;
        }

        public HostTypeInformationService(IHttpContextAccessor httpContextAccessor, PersistentComponentState persistentComponentState)
        {
            _httpContextAccessor = httpContextAccessor;
            _persistentComponentState = persistentComponentState;
        }
    }
}
