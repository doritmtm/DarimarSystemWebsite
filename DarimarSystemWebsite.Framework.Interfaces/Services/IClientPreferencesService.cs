namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IClientPreferencesService
    {
        public string? GetPreference(string name);
        public void SetPreference(string name, string value);
        public Task<string?> GetPreferenceAsync(string name);
        public Task SetPreferenceAsync(string name, string value);
    }
}
