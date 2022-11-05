using System;
using Cysharp.Threading.Tasks;
using GasSystem;
using UnityEngine;

namespace ItemSystem
{
    public class FanItem : Item
    {
        [SerializeField]
        private float _timeRotation;
        [SerializeField]
        private Vector3 _endRotation;
        [SerializeField]
        private Transform _rotateObject;
        [SerializeField]
        private float _delayBeforeHide;

        public override void Initialize(Gas gas, ItemManager itemManager)
        {
            base.Initialize(gas, itemManager);
            _gas.transform.localRotation = Quaternion.identity;
        }

        public override async UniTaskVoid Activate()
        {
            _rotateObject.eulerAngles = transform.eulerAngles;
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeActivation));
            
            _gas.LetOffTheGas();
            var startTime = 0f;
            var startRotation = transform.eulerAngles;
            var endRotation = startRotation + _endRotation;
            while (_timeRotation >= startTime && gameObject.activeInHierarchy)
            {
                startTime += Time.deltaTime;
                _rotateObject.eulerAngles = Vector3.Lerp(startRotation, endRotation, startTime / _timeRotation);

                await UniTask.Yield(PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeHide));
            _itemManager.ReleaseItem(this);
        }
    }
}