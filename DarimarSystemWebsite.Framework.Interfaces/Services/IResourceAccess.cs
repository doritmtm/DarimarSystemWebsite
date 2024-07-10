using System.Globalization;

namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IResourceAccess
    {
        public string? GetLocalizedString(string nameID, CultureInfo cultureInfo);
    }
}
