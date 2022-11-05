using System;
using Assets.Scripts.Managers.ScreensManager;
using SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.LoadScreen
{
    public class LoadingScreen : BaseScreenWithContext<LoadingScreenContext>
    {
        [SerializeField]
        private Image _imageLoadingProgress;

        private LoadSceneController _loadSceneController;
        
        public override void ApplyContext(LoadingScreenContext context)
        {
            _loadSceneController = context.LoadSceneController;
            _imageLoadingProgress.fillAmount = 0;
        }

        private void Update()
        {
            if (_loadSceneController != null)
            {
                _imageLoadingProgress.fillAmount = _loadSceneController.LoadingProgress;
            }
        }

        private void OnDisable()
        {
            _loadSceneController = null;
        }
    }

    public class LoadingScreenContext : BaseContext
    {
        public LoadSceneController LoadSceneController { get; }

        public LoadingScreenContext(LoadSceneController loadSceneController)
        {
            LoadSceneController = loadSceneController;
        }
    }
}