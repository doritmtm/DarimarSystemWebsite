using DarimarSystemWebsite.Framework.Interfaces.Services;
using System.Globalization;

namespace DarimarSystemWebsite.Resources
{
    public class ResourceAccess : IResourceAccess
    {
        public string? GetLocalizedString(string nameID, CultureInfo cultureInfo)
        {
            return SiteResources.ResourceManager.GetString(nameID, cultureInfo);
        }
    }
}
