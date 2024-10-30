using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Services
{
    public class PersistedPreferencesService : IPersistedPreferencesService
    {
        private IHostPreRenderingService _hostPreRenderingService;

        private PersistentComponentState? _persistentComponentState;

        private Dictionary<string, string?> _persistedPreferences = [];

        private List<string> _persistedKeys = [];

        public PersistedPreferencesService(IHostPreRenderingService hostPreRenderingService)
        {
            _hostPreRenderingService = hostPreRenderingService;
        }

        public void InitializePreferences(object state)
        {
            if (state is PersistentComponentState persistentComponentState)
            {
                _persistentComponentState = persistentComponentState;

                if (!_hostPreRenderingService.IsPreRendering)
                {
                    if (_persistentComponentState.TryTakeFromJson("persistedKeys", out List<string>? persistedKeys))
                    {
                        if (persistedKeys != null)
                        {
                            _persistedKeys = persistedKeys;

                            foreach (string persistedKey in persistedKeys)
                            {
                                if (_persistentComponentState.TryTakeFromJson(persistedKey, out string? value))
                                {
                                    if (value != null)
                                    {
                                        _persistedPreferences[persistedKey] = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string? GetPersistedPreference(string name)
        {
            return _persistedPreferences.ContainsKey(name) ? _persistedPreferences[name] : null;
        }

        public void PersistPreference(string name, string? value)
        {
            _persistedPreferences[name] = value;

            if (_persistentComponentState != null)
            {
                CommitToPersistingSystem();
            }
        }

        public void CommitToPersistingSystem()
        {
            if (_hostPreRenderingService.IsPreRendering)
            {
                if (_persistentComponentState != null)
                {
                    foreach (var preference in _persistedPreferences)
                    {
                        if (!_persistedKeys.Contains(preference.Key))
                        {
                            _persistentComponentState.RegisterOnPersisting(() =>
                            {
                                _persistentComponentState.PersistAsJson(preference.Key, preference.Value);
                                return Task.CompletedTask;
                            }, StaticSettings.GlobalRenderMode);
                            _persistedKeys.Add(preference.Key);
                        }
                    }

                    _persistentComponentState.RegisterOnPersisting(() =>
                    {
                        _persistentComponentState.PersistAsJson("persistedKeys", _persistedKeys);
                        return Task.CompletedTask;
                    }, StaticSettings.GlobalRenderMode);
                }
                else
                {
                    throw new NotSupportedException("PersistedPreferencesService: The service must be initialized using InitializePreferences");
                }
            }
        }
    }
}
