using DarimarSystemWebsite.Framework.Interfaces.Enums;
using DarimarSystemWebsite.Framework.Interfaces.Services;

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
            _darimarSystemService.ChangeLanguage(_darimarSystemService.CurrentLanguage);
        }
    }
}
