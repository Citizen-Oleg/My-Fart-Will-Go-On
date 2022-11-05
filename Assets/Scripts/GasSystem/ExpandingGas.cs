using System;
using Cysharp.Threading.Tasks;
using ItemSystem;
using PersonComponent;
using UnityEngine;

namespace GasSystem
{
    [RequireComponent(typeof(Collider))]
    public class ExpandingGas : Gas
    {
        [SerializeField]
        protected float _startRadius;
        [SerializeField]
        protected float _endRadius;
        [SerializeField]
        private float _increaseTime;
        [SerializeField]
        private SphereCollider _sphere;

        private bool _isActiveTrigger;
        private Collider _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
            _sphere.radius = _startRadius;
            _isActiveTrigger = false;
        }

        public override async UniTaskVoid LetOffTheGas()
        {
            _isActiveTrigger = true;
            _particleSystem.Play();
            
            ExpandScope().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(_particleSystem.main.duration));
            
            _gasPools.ReleaseGas(this);
            _sphere.radius = _startRadius;
            _collider.isTrigger = false;
            _isActiveTrigger = false;
        }

        protected override void ResetSettings()
        {
            _sphere.radius = _startRadius;
            _collider.isTrigger = false;
            _isActiveTrigger = false;
        }

        private async UniTaskVoid ExpandScope()
        {
            _collider.isTrigger = true;
            _sphere.radius = _startRadius;
            var currentTime = 0f;

            while (currentTime < _increaseTime && gameObject.activeInHierarchy)
            {
                currentTime += Time.deltaTime;
                _sphere.radius = Mathf.Lerp(_startRadius, _endRadius, currentTime / _increaseTime);

                await UniTask.Yield(PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isActiveTrigger && other.TryGetComponent(out Person person))
            {
                person.InfluenceGas(this);
            }
        }
    }
}