using System;
using System.Collections.Generic;
using Events;
using GasSystem;
using Tools.SimpleEventBus;
using UnityEngine;
using UpgradeSystem;

namespace FXUpgraderSystem
{
    public class UpgraderFXPools : IDisposable
    {
        private readonly Dictionary<TypeUpgrade, MonoBehaviourPool<FXUpgrader>> _fxUpgraders =
            new Dictionary<TypeUpgrade, MonoBehaviourPool<FXUpgrader>>();
        private readonly IDisposable _subscription;
        
        public UpgraderFXPools(Settings settings, Transform container)
        {
            _subscription = EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(ReleaseAll);
            foreach (var settingsFxUpgrader in settings.FxUpgraders)
            {
                var pool = new MonoBehaviourPool<FXUpgrader>(settingsFxUpgrader, container, settings.CountPool);
                _fxUpgraders.Add(settingsFxUpgrader.TypeUpgrade, pool);
            }
        }

        public FXUpgrader GetFX(TypeUpgrade typeUpgrade)
        {
            return _fxUpgraders[typeUpgrade].Take();
        }

        public void Release(FXUpgrader fxUpgrader)
        {
            _fxUpgraders[fxUpgrader.TypeUpgrade].Release(fxUpgrader);
        }
        
        private void ReleaseAll(EventLoadNextLevel eventLoadNextLevel)
        {
            foreach (var monoBehaviourPool in _fxUpgraders)
            {
                monoBehaviourPool.Value.ReleaseAll();
            }
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public int CountPool = 3;
            public List<FXUpgrader> FxUpgraders = new List<FXUpgrader>();
        }
    }
}