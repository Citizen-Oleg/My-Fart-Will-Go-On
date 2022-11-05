using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UpgradeSystem;

namespace FXUpgraderSystem
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FXUpgrader : MonoBehaviour
    {
        public TypeUpgrade TypeUpgrade => _typeUpgrade;
        
        [SerializeField]
        private TypeUpgrade _typeUpgrade;
        [SerializeField]
        private float _lifeTime;

        private ManagerFXUpgrader _managerFxUpgrader;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Initialize(ManagerFXUpgrader managerFxUpgrader)
        {
            _managerFxUpgrader = managerFxUpgrader;
        }

        public async UniTaskVoid Play()
        {
            _particleSystem.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));
            _managerFxUpgrader.ReleaseFX(this);
        }
    }
}