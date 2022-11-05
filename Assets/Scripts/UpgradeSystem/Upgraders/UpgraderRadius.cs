using System;
using System.Collections.Generic;
using System.Linq;
using ResourceSystem;

namespace UpgradeSystem.Upgraders
{
    public class UpgraderRadius : Upgrader
    {
        private readonly List<RadiusImprove> _radiusImproves;
        
        public UpgraderRadius(Settings settings, ResourceManagerLevel resourceManagerLevel) : base(resourceManagerLevel)
        {
            TypeUpgrade = settings.TypeUpgrade;
            _radiusImproves = settings.RadiusImproves;
        }

        public RadiusImprove GetCurrentUpgrades()
        {
            return _radiusImproves.FirstOrDefault(improves => improves.InformationImprove.Level == _currentLevel);
        }

        public override bool IsMaxImprove()
        {
            return _radiusImproves.All(improve => improve.InformationImprove.Level != _currentLevel + 1);
        }

        public override bool CanImprove()
        {
            var improve = GetNextImprove();
            return improve != null && _resourceManagerLevel.HasEnough(improve.InformationImprove.Resource);
        }

        public override InformationImprove GetInformationAboutTheCurrentImprovement()
        {
            foreach (var radiusImprove in _radiusImproves)
            {
                if (radiusImprove.InformationImprove.Level == _currentLevel)
                {
                    return radiusImprove.InformationImprove;
                }
            }

            return null;
        }

        public override InformationImprove GetInformationAboutTheNextImprovement()
        {
            foreach (var radiusImprove in _radiusImproves)
            {
                if (radiusImprove.InformationImprove.Level == _currentLevel + 1)
                {
                    return radiusImprove.InformationImprove;
                }
            }

            return null;
        }

        private RadiusImprove GetNextImprove()
        {
            return _radiusImproves.FirstOrDefault(improve => improve.InformationImprove.Level == _currentLevel + 1);
        }

        [Serializable]
        public class Settings
        {
            public TypeUpgrade TypeUpgrade;
            public List<RadiusImprove> RadiusImproves = new List<RadiusImprove>();
        }
        
        [Serializable]
        public class RadiusImprove
        {
            public InformationImprove InformationImprove;
            public float Radius;
            public float SpeedParticle;
        }
    }
}