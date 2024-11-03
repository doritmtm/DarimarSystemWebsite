using DarimarSystemWebsite.Framework.Interfaces.Services;
using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace DarimarSystemWebsite.Framework.Services
{
    public class PersistedPreferencesService : IPersistedPreferencesService
    {
        private IHostPreRenderingService _hostPreRenderingService;

        private PersistentComponentState? _persistentComponentState;

        private Dictionary<string, string?> _persistedPreferences = [];

        private Dictionary<string, object> _persistedObjects = [];

        private List<string> _persistedPreferencesKeys = [];

        private List<string> _persistedObjectsKeys = [];

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
                    if (_persistentComponentState.TryTakeFromJson("persistedPreferencesKeys", out List<string>? persistedPreferencesKeys))
                    {
                        if (persistedPreferencesKeys != null)
                        {
                            _persistedPreferencesKeys = persistedPreferencesKeys;

                            foreach (string persistedPreferenceKey in persistedPreferencesKeys)
                            {
                                if (_persistentComponentState.TryTakeFromJson(persistedPreferenceKey, out string? value))
                                {
                                    if (value != null)
                                    {
                                        _persistedPreferences[persistedPreferenceKey] = value;
                                    }
                                }
                            }
                        }
                    }

                    if (_persistentComponentState.TryTakeFromJson("persistedObjectsKeys", out List<string>? persistedObjectsKeys))
                    {
                        if (persistedObjectsKeys != null)
                        {
                            _persistedObjectsKeys = persistedObjectsKeys;

                            foreach (string persistedObjectKey in persistedObjectsKeys)
                            {
                                if (_persistentComponentState.TryTakeFromJson(persistedObjectKey, out object? value))
                                {
                                    if (value != null)
                                    {
                                        _persistedObjects[persistedObjectKey] = value;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    _persistentComponentState.RegisterOnPersisting(() =>
                    {
                        _persistentComponentState.PersistAsJson("persistedPreferencesKeys", _persistedPreferencesKeys);
                        return Task.CompletedTask;
                    }, StaticSettings.GlobalRenderMode);
                    _persistentComponentState.RegisterOnPersisting(() =>
                    {
                        _persistentComponentState.PersistAsJson("persistedObjectsKeys", _persistedObjectsKeys);
                        return Task.CompletedTask;
                    }, StaticSettings.GlobalRenderMode);
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
        public void RemovePersistedPreference(string name)
        {
            _persistedPreferences.Remove(name);
        }

        public PersistedType? GetPersistedObject<PersistedType>(string name)
        {
            return _persistedObjects.ContainsKey(name) ? JsonSerializer.Deserialize<PersistedType>((JsonElement)_persistedObjects[name]) : default;
        }

        public void PersistObject<PersistType>(string name, PersistType value)
        {
            _persistedObjects[name] = value!;

            if (_persistentComponentState != null)
            {
                CommitToPersistingSystem();
            }
        }

        public void RemovePersistedObject(string name)
        {
            _persistedObjects.Remove(name);
        }

        public void CommitToPersistingSystem()
        {
            if (_hostPreRenderingService.IsPreRendering)
            {
                if (_persistentComponentState != null)
                {
                    foreach (var preference in _persistedPreferences)
                    {
                        if (!_persistedPreferencesKeys.Contains(preference.Key))
                        {
                            _persistentComponentState.RegisterOnPersisting(() =>
                            {
                                _persistentComponentState.PersistAsJson(preference.Key, preference.Value);
                                return Task.CompletedTask;
                            }, StaticSettings.GlobalRenderMode);
                            _persistedPreferencesKeys.Add(preference.Key);
                        }
                    }

                    foreach (var obj in _persistedObjects)
                    {
                        if (!_persistedObjectsKeys.Contains(obj.Key))
                        {
                            _persistentComponentState.RegisterOnPersisting(() =>
                            {
                                _persistentComponentState.PersistAsJson(obj.Key, obj.Value);
                                return Task.CompletedTask;
                            }, StaticSettings.GlobalRenderMode);
                            _persistedObjectsKeys.Add(obj.Key);
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException("PersistedPreferencesService: The service must be initialized using InitializePreferences");
                }
            }
        }
    }
}
