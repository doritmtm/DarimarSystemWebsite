using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;

namespace DarimarSystemWebsite.Client
{
    public class WasmApp : IWasmApp
    {
        private IDarimarSystemService _darimarSystemService;

        public WasmApp(IDarimarSystemService darimarSystemService)
        {
            _darimarSystemService = darimarSystemService;
        }

        public void Initialize()
        {
            _darimarSystemService.HostType = HostTypeEnum.WebAssembly;
            StaticSettings.Version = _darimarSystemService.GetAppVersion();
        }
    }
}
