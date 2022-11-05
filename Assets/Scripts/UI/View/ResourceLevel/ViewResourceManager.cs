using System;
using Base;
using Base.MoveAnimation.UI;
using Events;
using PersonComponent;
using ResourceSystem;
using RewardSystem;
using Tools.SimpleEventBus;
using UnityEngine;

namespace View.ResourceLevel
{
    public class ViewResourceManager : IDisposable
    {
        private readonly ObjectPool _objectPool;
        private readonly ViewResourceLevel _viewResourceLevel;
        private readonly ResourceManagerLevel _resourceManagerLevel;
        private readonly RewardController _rewardController;
        private readonly Camera _camera;
        private readonly Settings _settings;
        private readonly AnimationManagerUI _animationManagerUi;
        private readonly IDisposable _subscription;

        public ViewResourceManager(Settings settings, ViewResourceLevel viewResourceLevel,
            RewardController rewardController, ResourceManagerLevel resourceManagerLevel, AnimationManagerUI animationManagerUi)
        {
            _settings = settings;
            _camera = Camera.main;
            _animationManagerUi = animationManagerUi;
            _resourceManagerLevel = resourceManagerLevel;
            _objectPool = new ObjectPool(settings.PrefabMoneyImage, settings.ParentCanvas, settings.PoolCapacity);
            _viewResourceLevel = viewResourceLevel;
            _rewardController = rewardController;

            _subscription = EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(StopAnimation);
            _resourceManagerLevel.OnPayResource += TakeAwayResources;
            _rewardController.OnChargingMoneyForVictims += AccrueResources;
        }

        private void AccrueResources(Transform transformObject, Resource resource)
        {
            var startPosition =
                UIUtility.WorldToCanvasPosition(_camera, _settings.ParentCanvas, transformObject.position);
            var money = _objectPool.Take();
            var rectTransform = money.transform as RectTransform;
            
            _animationManagerUi.ShowAnimationUI(rectTransform, startPosition, _viewResourceLevel.TextMoneyTransform,
                () =>
                {
                    _objectPool.Release(money);
                    _viewResourceLevel.AddResource(resource.Amount);
                });
        }

        private void StopAnimation(EventLoadNextLevel eventLoadNextLevel)
        {
            _animationManagerUi.StopAnimation();
            _objectPool.ReleaseAll();
        }

        private void TakeAwayResources(int price)
        {
            _viewResourceLevel.DecreaseResource(price);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _resourceManagerLevel.OnPayResource -= TakeAwayResources;
            _rewardController.OnChargingMoneyForVictims -= AccrueResources;
        }

        [Serializable]
        public class Settings
        {
            public RectTransform ParentCanvas;
            public GameObject PrefabMoneyImage;
            public int PoolCapacity;
        }
    }
}