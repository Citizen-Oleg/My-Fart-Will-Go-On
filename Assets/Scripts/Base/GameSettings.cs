using Base.MoveAnimation.UI;
using Emotion;
using FXUpgraderSystem;
using GasSystem;
using ItemSystem;
using Managers.ScreensManager;
using SaveLoadSystem;
using UI.Emotion;
using UnityEngine;
using Zenject;

namespace Base
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 0)]
    public class GameSettings : ScriptableObjectInstaller
    {
        [SerializeField]
        private AnimationManagerUI.Settings _settingsAnimationManagerUI;
        [SerializeField]
        private GasPools.Settings _settingsGasPools;
        [SerializeField]
        private ItemPools.Settings _settingsItemPools;
        [SerializeField]
        private UpgraderFXPools.Settings _settingsUpgraderFXPools;
        [Header("Emotion")]
        [SerializeField]
        private EmotionPools.Settings _settingsEmotionPools;
        [SerializeField]
        private EmotionFractionProvider.Settings _settingsEmotionFractionProvider;
        [SerializeField]
        private ScreenManager.Settings _settingsScreenManager;
        [SerializeField]
        private LoadController.Settings _settingsLoadController;
        [SerializeField] 
        private LoadSceneController.Settings _settingsLoadSceneController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_settingsEmotionPools);
            Container.BindInstance(_settingsEmotionFractionProvider);
            Container.BindInstance(_settingsUpgraderFXPools);
            Container.BindInstance(_settingsAnimationManagerUI);
            Container.BindInstance(_settingsGasPools);
            Container.BindInstance(_settingsItemPools);
            Container.BindInstance(_settingsScreenManager);
            Container.BindInstance(_settingsLoadController);
            Container.BindInstance(_settingsLoadSceneController);
        }
    }
}