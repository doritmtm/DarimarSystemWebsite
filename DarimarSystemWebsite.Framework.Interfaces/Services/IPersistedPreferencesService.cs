namespace DarimarSystemWebsite.Framework.Interfaces.Services
{
    public interface IPersistedPreferencesService
    {
        public void InitializePreferences(object state);
        public string? GetPersistedPreference(string name);
        public void PersistPreference(string name, string? value);
        public void RemovePersistedPreference(string name);
        public PersistedType? GetPersistedObject<PersistedType>(string name);
        public void PersistObject<PersistType>(string name, PersistType value);
        public void RemovePersistedObject(string name);
        public void CommitToPersistingSystem();
    }
}
