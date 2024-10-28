using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;

namespace DarimarSystemWebsite.Client
{
    public class ServerApp : IServerApp
    {
        private IDarimarSystemService _darimarSystemService;

        public ServerApp(IDarimarSystemService darimarSystemService)
        {
            _darimarSystemService = darimarSystemService;
        }

        public void Initialize()
        {
            _darimarSystemService.HostType = HostTypeEnum.Server;
            _darimarSystemService.InitializeLanguage();
            StaticSettings.Version = _darimarSystemService.GetAppVersion();
        }
    }
}
