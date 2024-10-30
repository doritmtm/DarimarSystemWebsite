namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IPersistedPreferencesService
    {
        public void InitializePreferences(object state);
        public string? GetPersistedPreference(string name);
        public void PersistPreference(string name, string? value);
        public void CommitToPersistingSystem();
    }
}
