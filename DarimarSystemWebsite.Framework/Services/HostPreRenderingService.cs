using DarimarSystemWebsite.Framework.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace DarimarSystemWebsite.Framework.Services
{
    public class HostPreRenderingService : IHostPreRenderingService
    {
        private IHttpContextAccessor? _httpContextAccessor;

        public bool IsPreRendering => !(_httpContextAccessor?.HttpContext?.WebSockets?.IsWebSocketRequest) ?? false;

        public HostPreRenderingService()
        {
        }

        public HostPreRenderingService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
