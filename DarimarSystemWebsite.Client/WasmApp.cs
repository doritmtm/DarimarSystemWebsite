using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

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
            _darimarSystemService.ChangeLanguage(_darimarSystemService.CurrentLanguage);
        }
    }
}
