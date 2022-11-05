using System;
using UpgradeSystem.Upgraders;

namespace GasSystem
{
    public class PlayerGasAdjuster : IDisposable
    {
        public UpgraderAutoFart.AutoFartImprove AutoFartImprove => _autoFartImprove;
        public UpgraderIntensity.IntensityImprove IntensityImprove => _intensityImprove;
        
        private readonly UpgraderRadius _upgraderRadius;
        private readonly UpgraderAutoFart _upgraderAutoFart;
        private readonly UpgraderIntensity _uprgaderIntensity;
        private readonly GasPools _gasPools;

        private UpgraderRadius.RadiusImprove _radiusImprove;
        private UpgraderIntensity.IntensityImprove _intensityImprove;
        private UpgraderAutoFart.AutoFartImprove _autoFartImprove;
         
        public PlayerGasAdjuster(UpgraderRadius upgraderRadius, UpgraderAutoFart upgraderAutoFart, UpgraderIntensity uprgaderIntensity, 
            GasPools gasPools)
        {
            _upgraderRadius = upgraderRadius;
            _upgraderAutoFart = upgraderAutoFart;
            _uprgaderIntensity = uprgaderIntensity;
            _gasPools = gasPools;

            SetRadiusImprove(_upgraderRadius);
            SetIntensityImprove(_uprgaderIntensity);
            SetAutoFartImprove(_upgraderAutoFart);
            
            _upgraderRadius.OnImprove += SetRadiusImprove;
            _uprgaderIntensity.OnImprove += SetIntensityImprove;
            _upgraderAutoFart.OnImprove += SetAutoFartImprove;
        }

        public PlayerGas GetPlayerGas()
        {
            var gas = _gasPools.GetGasByTypeGas(_intensityImprove.TypeGas);

            if (gas is PlayerGas playerGas)
            {
                playerGas.Level = _intensityImprove.InformationImprove.Level;
                playerGas.SetRadiusAction(_radiusImprove.Radius);
                SetRadiusGas(playerGas);
                return playerGas;
            }
            
            return null;
        }

        private void SetRadiusGas(PlayerGas playerGas)
        {
            foreach (var gasRadiusParticleSystem in playerGas.RadiusParticleSystems)
            {
                var mainModule = gasRadiusParticleSystem.main;
                mainModule.startSpeed = _radiusImprove.SpeedParticle;
            }
        }

        private void SetRadiusImprove(Upgrader upgrader)
        {
            _radiusImprove = _upgraderRadius.GetCurrentUpgrades();
        }
        
        private void SetIntensityImprove(Upgrader upgrader)
        {
            _intensityImprove = _uprgaderIntensity.GetCurrentUpgrades();
        }
        
        private void SetAutoFartImprove(Upgrader upgrader)
        {
            _autoFartImprove = _upgraderAutoFart.GetCurrentUpgrades();
        }

        public void Dispose()
        {
            _upgraderRadius.OnImprove -= SetRadiusImprove;
            _uprgaderIntensity.OnImprove -= SetIntensityImprove;
            _upgraderAutoFart.OnImprove -= SetAutoFartImprove;
        }
    }
}