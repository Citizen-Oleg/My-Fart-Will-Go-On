using FXUpgraderSystem;
using ItemSystem;
using Joystick_and_Swipe;
using ResourceSystem;
using RewardSystem;
using UnityEngine;
using UpgradeSystem;
using UpgradeSystem.Upgraders;
using Zenject;

namespace Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private UpgraderFXPools.Settings _settingsUpgraderFXPools;
        [SerializeField]
        private JoystickController _joystickController;
        [SerializeField]
        private ResourceManagerLevel.Settings _settingsResourceManagerLevel;
        [SerializeField]
        private LevelSettings _levelSettings;
        [SerializeField]
        private ClickHandler.Settings _settingsClickHandler;
        [SerializeField]
        private RewardController.Settings _settingsRewardController;
        [Header("Upgraders settings")]
        [SerializeField]
        private UpgraderIntensity.Settings _settingsUpgraderIntensity;
        [SerializeField]
        private UpgraderRadius.Settings _settingsUpgraderRadius;
        [SerializeField]
        private UpgraderAutoFart.Settings _settingsUpgraderAutoFart;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsResourceManagerLevel);
            Container.BindInstance(_settingsRewardController);
            Container.BindInstance(_levelSettings);
            Container.BindInstance(_settingsClickHandler);
            Container.BindInstance(_settingsUpgraderIntensity);
            Container.BindInstance(_settingsUpgraderRadius);
            Container.BindInstance(_settingsUpgraderAutoFart);
            Container.BindInstance(_settingsUpgraderFXPools);
            
            Container.BindInstance(_joystickController).AsSingle();
            
            Container.BindInterfacesAndSelfTo<ResourceManagerLevel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClickHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RewardController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ManagerFXUpgrader>().AsSingle().NonLazy();

            Container.Bind<UpgraderIntensity>().AsSingle().NonLazy();
            Container.Bind<UpgraderRadius>().AsSingle().NonLazy();
            Container.Bind<UpgraderAutoFart>().AsSingle().NonLazy();
            Container.Bind<UpgradeManager>().AsSingle().NonLazy();
            Container.Bind<ItemManager>().AsSingle().NonLazy();
        }
    }
}