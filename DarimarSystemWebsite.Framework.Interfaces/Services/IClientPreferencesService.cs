namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IClientPreferencesService
    {
        public bool? CookieUserConsent { get; set; }
        public Task Initialize();
        public Task<string?> GetPreferenceAsync(string name);
        public Task SetPreferenceAsync(string name, string value);
        public void ResetAllPreferences();
    }
}
