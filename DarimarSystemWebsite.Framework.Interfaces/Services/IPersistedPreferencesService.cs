namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IPersistedPreferencesService
    {
        public string? GetPersistedPreference(string name);
        public void PersistPreference(string name, string? value);
    }
}
