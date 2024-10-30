namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IClientPreferencesService
    {
        public Task<string?> GetPreferenceAsync(string name);
        public Task SetPreferenceAsync(string name, string value);
    }
}
