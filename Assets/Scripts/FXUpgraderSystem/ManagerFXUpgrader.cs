using System;
using PlayerComponent;
using UpgradeSystem.Upgraders;

namespace FXUpgraderSystem
{
    public class ManagerFXUpgrader : IDisposable
    {
        private readonly Player _player;
        private readonly UpgraderIntensity _upgraderIntensity;
        private readonly UpgraderRadius _upgraderRadius;
        private readonly UpgraderAutoFart _upgraderAutoFart;
        private readonly UpgraderFXPools _upgraderFxPools;

        public ManagerFXUpgrader(Player player, UpgraderIntensity upgraderIntensity, UpgraderRadius upgraderRadius,
            UpgraderAutoFart upgraderAutoFart, UpgraderFXPools upgraderFxPools)
        {
            _player = player;
            _upgraderIntensity = upgraderIntensity;
            _upgraderRadius = upgraderRadius;
            _upgraderAutoFart = upgraderAutoFart;
            _upgraderFxPools = upgraderFxPools;

            _upgraderIntensity.OnImprove += CreateFX;
            _upgraderRadius.OnImprove += CreateFX;
            _upgraderAutoFart.OnImprove += CreateFX;
        }

        private void CreateFX(Upgrader upgrader)
        {
            var fx = _upgraderFxPools.GetFX(upgrader.TypeUpgrade);
            fx.transform.parent = _player.FxPosition;
            fx.transform.position = _player.FxPosition.position;
            fx.Initialize(this);
            fx.Play();
        }

        public void ReleaseFX(FXUpgrader fxUpgrader)
        {
            _upgraderFxPools.Release(fxUpgrader);
        }

        public void Dispose()
        {
            _upgraderIntensity.OnImprove -= CreateFX;
            _upgraderRadius.OnImprove -= CreateFX;
            _upgraderAutoFart.OnImprove -= CreateFX;
        }
    }
}