using System;
using GasSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PersonComponent
{
    public class AccumulatorDisgust
    {
        public event Action OnChangePatience;
        public event Action<Gas> OnLimitPatience;
        
        public int MaxPatience => _patience;
        public float CurrentPatience => _currentPatience;
        
        private readonly int _patience;

        private float _currentPatience;

        public AccumulatorDisgust(int minimumPatience, int maximumPatience)
        {
            _patience = Random.Range(minimumPatience, maximumPatience);
        }

        public void Accumulator(Gas gas)
        {
            _currentPatience = Mathf.Clamp(gas.PowerStink + _currentPatience, 0, _patience);
            OnChangePatience?.Invoke();
            
            if (_currentPatience >= _patience)
            {
                OnLimitPatience?.Invoke(gas);
            }
        }
    }
}