using System;
using System.Collections.Generic;
using Events;
using Tools.SimpleEventBus;
using UnityEngine;

namespace GasSystem
{
    public class GasPools : IDisposable
    {
        private readonly Dictionary<TypeGas, MonoBehaviourPool<Gas>> _gases = new Dictionary<TypeGas, MonoBehaviourPool<Gas>>();
        private readonly IDisposable _subscription;
        
        public GasPools(Settings settings, Transform container)
        {
            _subscription = EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(ReleaseAll);
            
            foreach (var gas in settings.GasPrefabs)
            {
                var pool = new MonoBehaviourPool<Gas>(gas , container, settings.CountPool);
                _gases.Add(gas.TypeGas, pool);
            }
        }

        public Gas GetGasByTypeGas(TypeGas typeGas)
        {
            var gas = _gases[typeGas].Take();
            gas.Initialize(this);
            return gas;
        }

        public void ReleaseGas(Gas gas)
        {
            _gases[gas.TypeGas].Release(gas);
        }

        private void ReleaseAll(EventLoadNextLevel eventLoadNextLevel)
        {
            foreach (var monoBehaviourPool in _gases)
            {
                monoBehaviourPool.Value.ReleaseAll();
            }
        }

        [Serializable]
        public class Settings
        {
            public int CountPool = 3;
            public List<Gas> GasPrefabs = new List<Gas>();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}