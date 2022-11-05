using System;
using GasSystem;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class ExhaustGasController : ITickable
    {
        public event Action<float> OnRefreshTimer;

        private bool IsCooldownAutoFart => _startTimeCooldownAutoFart >= Time.time;

        private readonly Transform _graduationPosition;
        private readonly PlayerGasAdjuster _playerGasAdjuster;
        
        private float _startTimeCooldownAutoFart;

        public ExhaustGasController(Settings settings, PlayerGasAdjuster playerGasAdjuster)
        {
            _playerGasAdjuster = playerGasAdjuster;
            _graduationPosition = settings.GraduationPosition;
        }
        
        public void Tick()
        {
            if (_playerGasAdjuster.AutoFartImprove.IsActive)
            {
                if (IsCooldownAutoFart)
                {
                    return;
                }

                var cooldown = _playerGasAdjuster.AutoFartImprove.Cooldown;
                OnRefreshTimer?.Invoke(cooldown);
                _startTimeCooldownAutoFart = Time.time + cooldown;
                LetOffTheGas();
            }
        }
        
        public void ManualGasActivation()
        {
            LetOffTheGas();
        }

        private void LetOffTheGas()
        {
            var gas = _playerGasAdjuster.GetPlayerGas();
            gas.transform.position = _graduationPosition.position;
            gas.LetOffTheGas();
        }
        

        [Serializable]
        public class Settings
        {
            public Transform GraduationPosition;
        }
    }
}