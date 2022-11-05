using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using GasSystem;
using ResourceSystem;
using Tools.SimpleEventBus;
using UnityEngine;

namespace RewardSystem
{
    public class RewardController : IDisposable
    {
        public event Action<Transform, Resource> OnChargingMoneyForVictims;
        
        private readonly IDisposable _subscription;
        private readonly ResourceManagerLevel _resourceManagerLevel;
        private readonly List<RewardMultiplier> _rewardMultipliers;

        public RewardController(Settings settings, ResourceManagerLevel resourceManagerLevel)
        {
            _rewardMultipliers = settings.RewardMultipliers;
            _resourceManagerLevel = resourceManagerLevel;
            _subscription = EventStreams.UserInterface.Subscribe<EventNewFartVictims>(Reward);
        }

        private void Reward(EventNewFartVictims eventNewFartVictims)
        {
            var reward = CalculateResource(eventNewFartVictims.Gas, eventNewFartVictims.ResourceObject.Resource);
            _resourceManagerLevel.AddResource(reward);
            OnChargingMoneyForVictims?.Invoke(eventNewFartVictims.EmotionPerson.Transform, reward);
        }

        private Resource CalculateResource(Gas gas, Resource resource)
        {
            var multiplier = GetMultiplier(gas);
            float amount = resource.Amount;
            amount *= multiplier;
            resource.Amount = (int) amount;
            return resource;
        }

        private float GetMultiplier(Gas gas)
        {
            return _rewardMultipliers.FirstOrDefault(reward => reward.Level == gas.Level).Multiplier;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public List<RewardMultiplier> RewardMultipliers = new List<RewardMultiplier>();
        }

        [Serializable]
        public class RewardMultiplier
        {
            public int Level;
            public float Multiplier;
        }
    }
}