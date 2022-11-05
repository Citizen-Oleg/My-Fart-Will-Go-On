using Base.MoveAnimation.UI;
using Emotion;
using FXUpgraderSystem;
using GasSystem;
using ItemSystem;
using Managers.ScreensManager;
using SaveLoadSystem;
using UI.Emotion;
using Zenject;

namespace Base
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EmotionPools>().AsSingle().WithArguments(transform).NonLazy();
            Container.BindInterfacesAndSelfTo<GasPools>().AsSingle().WithArguments(transform).NonLazy();
            Container.BindInterfacesAndSelfTo<ItemPools>().AsSingle().WithArguments(transform).NonLazy();
            Container.BindInterfacesAndSelfTo<UpgraderFXPools>().AsSingle().WithArguments(transform).NonLazy();
            Container.Bind<EmotionFractionProvider>().AsSingle().NonLazy();
            Container.Bind<ScreenManager>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<EmotionManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnimationManagerUI>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LoadSceneController>().AsSingle().NonLazy();
            Container.Bind<SaveController>().AsSingle().NonLazy();
            Container.Bind<LoadController>().AsSingle().NonLazy();
        }
    }
}
