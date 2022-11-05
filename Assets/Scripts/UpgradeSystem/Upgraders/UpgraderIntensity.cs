using System;
using System.Collections.Generic;
using System.Linq;
using GasSystem;
using ResourceSystem;

namespace UpgradeSystem.Upgraders
{
    public class UpgraderIntensity : Upgrader
    {
        private readonly List<IntensityImprove> _intensityImproves;
        
        public UpgraderIntensity(Settings settings, ResourceManagerLevel resourceManagerLevel) : base(resourceManagerLevel)
        {
            TypeUpgrade = settings.TypeUpgrade;
            _intensityImproves = settings.IntensityImproves;
        }
        
        public IntensityImprove GetCurrentUpgrades()
        {
            return _intensityImproves.FirstOrDefault(improves => improves.InformationImprove.Level == _currentLevel);
        }

        public override bool IsMaxImprove()
        {
            return _intensityImproves.All(improve => improve.InformationImprove.Level != _currentLevel + 1);
        }

        public override bool CanImprove()
        {
            var improve = GetNextImprove();
            return improve != null && _resourceManagerLevel.HasEnough(improve.InformationImprove.Resource);
        }

        public override InformationImprove GetInformationAboutTheCurrentImprovement()
        {
            foreach (var intensityImprove in _intensityImproves)
            {
                if (intensityImprove.InformationImprove.Level == _currentLevel)
                {
                    return intensityImprove.InformationImprove;
                }
            }

            return null;
        }

        public override InformationImprove GetInformationAboutTheNextImprovement()
        {
            foreach (var intensityImprove in _intensityImproves)
            {
                if (intensityImprove.InformationImprove.Level == _currentLevel + 1)
                {
                    return intensityImprove.InformationImprove;
                }
            }

            return null;
        }

        private IntensityImprove GetNextImprove()
        {
            return _intensityImproves.FirstOrDefault(improve => improve.InformationImprove.Level == _currentLevel + 1);
        }

        [Serializable]
        public class Settings
        {
            public TypeUpgrade TypeUpgrade;
            public List<IntensityImprove> IntensityImproves = new List<IntensityImprove>();
        }

        [Serializable]
        public class IntensityImprove
        {
            public InformationImprove InformationImprove;
            public TypeGas TypeGas;
        }
    }
}