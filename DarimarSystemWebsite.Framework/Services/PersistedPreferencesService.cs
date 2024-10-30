using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Services
{
    public class PersistedPreferencesService : IPersistedPreferencesService
    {
        private PersistentComponentState _persistentComponentState;

        private IHostPreRenderingService _hostPreRenderingService;

        private Dictionary<string, string?> _persistedPreferences = [];

        public PersistedPreferencesService(PersistentComponentState persistentComponentState, IHostPreRenderingService hostPreRenderingService)
        {
            _persistentComponentState = persistentComponentState;
            _hostPreRenderingService = hostPreRenderingService;
        }

        public string? GetPersistedPreference(string name)
        {
            if (!_hostPreRenderingService.IsPreRendering)
            {
                if (!_persistedPreferences.ContainsKey(name))
                {
                    if (_persistentComponentState.TryTakeFromJson(name, out string? value))
                    {
                        if (value != null)
                        {
                            _persistedPreferences[name] = value;
                        }
                    }
                }
            }

            return _persistedPreferences.ContainsKey(name) ? _persistedPreferences[name] : null;
        }

        public void PersistPreference(string name, string? value)
        {
            if (_hostPreRenderingService.IsPreRendering)
            {
                if (!_persistedPreferences.ContainsKey(name))
                {
                    _persistentComponentState.RegisterOnPersisting(() =>
                    {
                        _persistentComponentState.PersistAsJson(name, value);
                        return Task.CompletedTask;
                    }, StaticSettings.GlobalRenderMode);
                }
            }

            _persistedPreferences[name] = value;
        }
    }
}
