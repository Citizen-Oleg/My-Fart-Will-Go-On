using GasSystem;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _player;
        [SerializeField]
        private PlayerPerceptionTriggerZone _playerPerceptionTriggerZone;
        [SerializeField]
        private Player.Settings _playerSettings;
        [SerializeField]
        private ExhaustGasController.Settings _settingsExhaustGasController;
        [SerializeField]
        private PlayerItemInstaller.Settings _settingsPlayerItemInstaller;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings);
            Container.BindInstance(_settingsExhaustGasController);
            Container.BindInstance(_settingsPlayerItemInstaller);

            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_playerPerceptionTriggerZone).AsSingle();

            Container.BindInterfacesAndSelfTo<ExhaustGasController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MovementController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerGasAdjuster>().AsSingle().NonLazy();

            Container.Bind<PlayerItemInstaller>().AsSingle().NonLazy();
        }
    }
}