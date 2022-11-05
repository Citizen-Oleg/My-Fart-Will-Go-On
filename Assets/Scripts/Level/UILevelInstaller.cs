using Base.MoveAnimation.UI;
using UI.Disgusted;
using UI.ItemShop;
using UI.Upgrade;
using UI.View;
using UnityEngine;
using UnityEngine.Serialization;
using UpgradeSystem.UI;
using View;
using View.ResourceLevel;
using Zenject;

namespace Level
{
	public class UILevelInstaller : MonoInstaller
	{
		[SerializeField]
		private ViewResourceManager.Settings _settingsViewResourceManager;
		[SerializeField]
		private ViewResourceLevel.Settings _settingsViewResourceLevel;
		[SerializeField]
		private ViewResourceAnimationController.Settings _settingsViewResourceAnimationController;
		[SerializeField]
		private LevelCompletionBar.Settings _settingsLevelCompletionBar;
		[Header("Upgrade settings")]
		[SerializeField]
		private UIUpgradeManager.Settings _settingsUIUpgradeManager;
		[SerializeField]
		private ColorUpgradeButtonProvider.Settings _settingsColorStateButtonProvider;
		[SerializeField]
		private ViewTimer.Settings _settingsViewTimer;
		[Header("Item settings")]
		[SerializeField]
		private ColorItemButtonProvider.Settings _settingsColorItemButtonProvider;
		[SerializeField]
		private UIItemManager.Settings _settingsUIItemManager;
		[SerializeField]
		private ViewDisgustedManager.Settings _settingsViewDisgustedManager;

		public override void InstallBindings()
		{
			Container.BindInstance(_settingsViewResourceLevel);
			Container.BindInstance(_settingsViewResourceAnimationController);
			Container.BindInstance(_settingsViewResourceManager);
			Container.BindInstance(_settingsLevelCompletionBar);
			Container.BindInstance(_settingsUIUpgradeManager);
			Container.BindInstance(_settingsColorStateButtonProvider);
			Container.BindInstance(_settingsViewTimer);
			Container.BindInstance(_settingsColorItemButtonProvider);
			Container.BindInstance(_settingsUIItemManager);
			Container.BindInstance(_settingsViewDisgustedManager);

			Container.Bind<ColorUpgradeButtonProvider>().AsSingle().NonLazy();
			Container.Bind<ColorItemButtonProvider>().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<ViewResourceLevel>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<LevelCompletionBar>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<ViewResourceAnimationController>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<ViewResourceManager>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<UIUpgradeManager>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<ViewTimer>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<UIItemManager>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<ViewDisgustedManager>().AsSingle().NonLazy();
		}
	}
}