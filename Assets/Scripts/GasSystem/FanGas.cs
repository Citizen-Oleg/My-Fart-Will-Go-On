using System;
using Cysharp.Threading.Tasks;
using PersonComponent;
using UnityEngine;

namespace GasSystem
{
    [RequireComponent(typeof(Collider))]
    public class FanGas : Gas
    {
        [SerializeField]
        private float _increaseTime;
        [SerializeField]
        private Vector3 _startSize;
        [SerializeField]
        private Vector3 _endSize;
        [SerializeField]
        private Vector3 _startCenterPosition;
        [SerializeField]
        private Vector3 _endCenterPosition;
        [SerializeField]
        private BoxCollider _boxCollider;
        
        private bool _isActiveTrigger;

        protected override void Awake()
        {
            base.Awake();
            GetComponent<Collider>().isTrigger = true;
            
            _boxCollider.center = _startCenterPosition;
            _boxCollider.size = _startSize;
            _isActiveTrigger = false;
        }
        
        public override async UniTaskVoid LetOffTheGas()
        {
            _isActiveTrigger = true;
            _particleSystem.Play();
            ExpandScope().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(_particleSystem.main.duration));
            _gasPools.ReleaseGas(this);
            
            _boxCollider.center = _startCenterPosition;
            _boxCollider.size = _startSize;
            _isActiveTrigger = false;
        }

        protected override void ResetSettings()
        {
            _boxCollider.center = _startCenterPosition;
            _boxCollider.size = _startSize;
            _isActiveTrigger = false;
        }

        private async UniTaskVoid ExpandScope()
        {
            _boxCollider.center = _startCenterPosition;
            _boxCollider.size = _startSize;
            
            var currentTime = 0f;

            while (currentTime < _increaseTime && gameObject.activeInHierarchy)
            {
                _boxCollider.center = Vector3.Lerp(_startCenterPosition, _endCenterPosition, currentTime / _increaseTime);
                _boxCollider.size = Vector3.Lerp(_startSize, _endSize, currentTime / _increaseTime);

                currentTime += Time.deltaTime;
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