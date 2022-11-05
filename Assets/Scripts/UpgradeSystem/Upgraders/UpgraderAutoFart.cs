using System;
using System.Collections.Generic;
using System.Linq;
using ResourceSystem;

namespace UpgradeSystem.Upgraders
{
    public class UpgraderAutoFart : Upgrader
    {
        private readonly List<AutoFartImprove> _autoFartImproves;
        
        public UpgraderAutoFart(Settings settings, ResourceManagerLevel resourceManagerLevel) : base(resourceManagerLevel)
        {
            TypeUpgrade = settings.TypeUpgrade;
            _autoFartImproves = settings.AutoFartImproves;
        }
        
        public AutoFartImprove GetCurrentUpgrades()
        {
            return _autoFartImproves.FirstOrDefault(improves => improves.InformationImprove.Level == _currentLevel);
        }

        public override bool IsMaxImprove()
        {
            return _autoFartImproves.All(improve => improve.InformationImprove.Level != _currentLevel + 1);
        }

        public override bool CanImprove()
        {
            var improve = GetNextImprove();
            return improve != null && _resourceManagerLevel.HasEnough(improve.InformationImprove.Resource);
        }

        public override InformationImprove GetInformationAboutTheCurrentImprovement()
        {
            foreach (var autoFartImprove in _autoFartImproves)
            {
                if (autoFartImprove.InformationImprove.Level == _currentLevel)
                {
                    return autoFartImprove.InformationImprove;
                }
            }

            return null;
        }

        public override InformationImprove GetInformationAboutTheNextImprovement()
        {
            foreach (var autoFartImprove in _autoFartImproves)
            {
                if (autoFartImprove.InformationImprove.Level == _currentLevel + 1)
                {
                    return autoFartImprove.InformationImprove;
                }
            }

            return null;
        }

        private AutoFartImprove GetNextImprove()
        {
            return _autoFartImproves.FirstOrDefault(improve => improve.InformationImprove.Level == _currentLevel + 1);
        }
        
        [Serializable]
        public class Settings
        {
            public TypeUpgrade TypeUpgrade;
            public List<AutoFartImprove> AutoFartImproves = new List<AutoFartImprove>();
        }

        [Serializable]
        public class AutoFartImprove
        {
            public InformationImprove InformationImprove;
            public bool IsActive = true;
            public float Cooldown;
        }
    }
}