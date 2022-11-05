using System;
using System.Collections.Generic;
using System.Linq;
using UpgradeSystem.Upgraders;

namespace UpgradeSystem
{
    public class UpgradeManager
    {
        private readonly List<Upgrader> _upgraders;
        
        public UpgradeManager(UpgraderRadius upgraderRadius, UpgraderIntensity upgraderIntensity, UpgraderAutoFart upgraderAutoFart)
        {
            _upgraders = new List<Upgrader>
            {
                upgraderIntensity, upgraderRadius, upgraderAutoFart
            };
        }

        public bool CanImprove(TypeUpgrade typeUpgrade)
        {
            foreach (var upgrader in _upgraders)
            {
                if (upgrader.TypeUpgrade == typeUpgrade && upgrader.CanImprove())
                {
                    return true;
                }
            }

            return false;
        }

        public bool Improve(TypeUpgrade typeUpgrade)
        {
            if (!IsMaxImprove(typeUpgrade) && CanImprove(typeUpgrade))
            {
                var upgrader = GetUpgraderByTypeUpgrade(typeUpgrade);
                upgrader.Improve();
                return true;
            }

            return false;
        }

        public Upgrader.InformationImprove GetCurrentInformationImprove(TypeUpgrade typeUpgrade)
        {
            var upgrader = GetUpgraderByTypeUpgrade(typeUpgrade);
            return upgrader.GetInformationAboutTheCurrentImprovement();
        }
        
        public Upgrader.InformationImprove GetNextInformationImprove(TypeUpgrade typeUpgrade)
        {
            var upgrader = GetUpgraderByTypeUpgrade(typeUpgrade);
            return upgrader.GetInformationAboutTheNextImprovement();
        }

        public bool IsMaxImprove(TypeUpgrade typeUpgrade)
        {
            var upgrader = GetUpgraderByTypeUpgrade(typeUpgrade);
            return upgrader.IsMaxImprove();
        }

        private Upgrader GetUpgraderByTypeUpgrade(TypeUpgrade typeUpgrade)
        {
            return _upgraders.FirstOrDefault(upgreder => upgreder.TypeUpgrade == typeUpgrade);
        }
    }
}